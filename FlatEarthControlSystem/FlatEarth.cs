using System;
using System.Linq;
using FlatEarthControlSystem.ControlCommandParser;
using FlatEarthControlSystem.ControlCommandParser.Words;
using FlatEarthControlSystem.PostProcessing;
using FlatEarthControlSystem.PreProcessing;
using FlatEarthControlSystem.WorldDefinition;
using FlatEarthControlSystem.WorldDefinitionParser;

namespace FlatEarthControlSystem
{
    public class FlatEarth
    {
        public static readonly Verbs Verbs;
        public static readonly KnownFills KnownFills;

        public PreProcessor? CustomPreProcessor;
        public PostProcessor? CustomPostProcessor;
        
        public World World { get; private set; }
        public Player Player { get; private set; }
        public WorldObjectList WorldObjects { get; }


        static FlatEarth()
        {
            Verbs = new Verbs();
            KnownFills = new KnownFills();
        }
        
        public FlatEarth()
        {
            World = new World();
            WorldObjects = new WorldObjectList();
            Player = new Player(this);
            Uppercase = false;
        }

        public bool Uppercase
        {
            get => World.Uppercase;
            set => World.Uppercase = value;
        }

        public void Load(string data)
        {
            var worldParser = new WorldParser(data);
            World = worldParser.Parse(out var startRoomId);
            Player = new Player(this);
            Player.SetCurrentRoomId(startRoomId, World);
        }

        public string SetCurrentRoomId(string currentRoomId)
        {
            var room = Player.SetCurrentRoomId(currentRoomId, World);
            return room.GetDescription();
        }

        public CommandResult Do(string command)
        {
            var result = new CommandParser(GetCurrentRoom(), command).Parse();
            var preProcessor = CustomPreProcessor;
            
            if (!result.Success)
            {
                if (CustomPreProcessor == null)
                    return Fail(string.IsNullOrWhiteSpace(result.Message) ? Case(StandardAnswers.IdontUnderstand) : result.Message);

                var preProcessorArguments = new PreProcessorArguments
                {
                    SourceText = command,
                    PreProcessorIntention = PreProcessorIntention.ParseFailed
                };

                CustomPreProcessor.Invoke(preProcessorArguments);

                if (preProcessorArguments.Cancel)
                    return Fail(string.IsNullOrWhiteSpace(preProcessorArguments.CancelText)
                        ? Case(StandardAnswers.IdontUnderstand)
                        : preProcessorArguments.CancelText);

                preProcessor = null;
            }

            if (preProcessor != null)
            {
                
            }
            
            switch (result.Intention)
            {
                case PreProcessorIntention.Inventory:
                    return Inventory();
                case PreProcessorIntention.Move:
                    return Go(result.Result!.Part2Noun);
                case PreProcessorIntention.Exits:
                    return GetExits();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public CommandResult Go(Noun direction)
        {
            var room = GetCurrentRoom();
            var exit = room.GetDiscoveredExit(direction.StringRepresentation);
            if (exit == null)
                return Fail(Case(StandardAnswers.YouCantGoThatWay));
            //TODO: Check conditions.
            room = Player.SetCurrentRoomId(exit.TargetRoomId, World);
            var roomDescription = room.GetDescription();
            return Success(
                string.IsNullOrWhiteSpace(roomDescription)
                    ? Case(StandardAnswers.Ok)
                    : roomDescription
            );
        }

        public CommandResult Look() =>
            Success(
                GetCurrentRoom()
                    .GetLookText()
            );
        
        public CommandResult GetExits() =>
            Success(
                new ExitList(
                    GetCurrentRoom()
                        .GetDiscoveredExits()
                        .Where(x => x.Discovered)
                ).ToString()
            );

        public CommandResult Inventory() =>
            Player.Inventory.Empty()
                ? new CommandResult(true, Phrases.YouAreNotCarryingAnything)
                : new CommandResult(true, $"{Phrases.YouAreNotCarryingAnything}{Player.Inventory.EnumerationText}.");

        public Room GetCurrentRoom()
        {
            var currentRoom = World.GetRoom(Player.GetCurrentRoomId());
            if (currentRoom == null)
                throw new SystemException("Room does not exist.");
            return currentRoom;
        }
        
        private static CommandResult Fail(string text) =>
            new CommandResult(false, text);
        
        private static CommandResult Success(string text) =>
            new CommandResult(true, text);

        private string Case(string s) =>
            Uppercase
                ? s.ToUpper()
                : s;
    }
}
using System;
using System.ComponentModel;
using System.Linq;
using FlatEarthControlSystem.ControlCommandParser;
using FlatEarthControlSystem.ControlCommandParser.WordTypes;
using FlatEarthControlSystem.PostProcessing;
using FlatEarthControlSystem.PreProcessing;
using FlatEarthControlSystem.WorldDefinition;
using FlatEarthControlSystem.WorldDefinitionParser;

namespace FlatEarthControlSystem
{
    public class FlatEarth
    {
        public PreProcessor? CustomPreProcessor;
        public PostProcessor? CustomPostProcessor;
        
        public World World { get; private set; }
        public Player Player { get; private set; }
        public WorldObjectList WorldObjects { get; }


        static FlatEarth()
        {
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
            var currentRoom = GetCurrentRoom();

            var result = new CommandParser(
                World,
                currentRoom,
                Player.Inventory
            ).Parse(command);

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
                //TODO
            }
            
            var executer = new CommandExecuter(
                Player,
                World,
                currentRoom,
                Uppercase
            );

            return executer.Apply(result);
        }

        public Room GetCurrentRoom()
        {
            var currentRoom = World.GetRoom(Player.GetCurrentRoomId());
            if (currentRoom == null)
                throw new SystemException("Room does not exist.");
            return currentRoom;
        }

        private static CommandResult Fail(string text) =>
            new CommandResult(false, text);

        private string Case(string s) =>
            Uppercase
                ? s.ToUpper()
                : s;
    }
}
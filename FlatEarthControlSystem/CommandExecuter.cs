using System;
using System.Linq;
using FlatEarthControlSystem.ControlCommandParser;
using FlatEarthControlSystem.ControlCommandParser.WordTypes;
using FlatEarthControlSystem.ControlCommandParser.WordTypes.Nouns;
using FlatEarthControlSystem.PreProcessing;
using FlatEarthControlSystem.WorldDefinition;

namespace FlatEarthControlSystem
{
    public class CommandExecuter
    {
        private readonly World _world;
        private readonly Player _player;
        private readonly Room _currentRoom;
        private readonly bool _uppercase;

        public CommandExecuter(Player player, World world, Room currentRoom, bool uppercase)
        {
            _player = player;
            _world = world;
            _currentRoom = currentRoom;
            _uppercase = uppercase;
        }

        public CommandResult Apply(CommandParserResult result) =>
            result.Intention switch
            {
                PreProcessorIntention.Inventory => Inventory(),
                PreProcessorIntention.Move => Go(result.Result?.Word03Noun),
                PreProcessorIntention.Exits => GetExits(),
                _ => throw new ArgumentOutOfRangeException()
            };

        public CommandResult Go(Noun? direction)
        {
            if (direction == null)
                return Fail(StandardAnswers.YouCantGoThatWay);

            var dir = Direction.FromNoun(direction);

            if (dir == null)
                return Fail(StandardAnswers.YouCantGoThatWay);

            var exit = _currentRoom.GetDiscoveredExit(dir);
            if (exit == null)
                return Fail(Case(StandardAnswers.YouCantGoThatWay));

            //TODO: Check conditions.

            var room = _player.SetCurrentRoomId(exit.TargetRoomId, _world);
            var roomDescription = room.GetDescription();

            return Success(
                string.IsNullOrWhiteSpace(roomDescription)
                    ? Case(StandardAnswers.Ok)
                    : roomDescription
            );
        }

        public CommandResult Look() =>
            Success(
                _currentRoom.GetLookText()
            );

        public CommandResult GetExits() =>
            Success(
                new ExitList(
                    _currentRoom
                        .GetDiscoveredExits()
                        .Where(x => x.Discovered)
                ).ToString()
            );

        public CommandResult Inventory() =>
            _player.Inventory.Empty()
                ? new CommandResult(true, Phrases.YouAreNotCarryingAnything)
                : new CommandResult(true, $"{Phrases.YouAreNotCarryingAnything}{_player.Inventory.EnumerationText}.");

        private static CommandResult Fail(string text) =>
            new CommandResult(false, text);

        private static CommandResult Success(string text) =>
            new CommandResult(true, text);

        private string Case(string s) =>
            _uppercase
                ? s.ToUpper()
                : s;
    }
}
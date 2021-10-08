using System;
using System.Linq;
using FlatEarthControlSystem.Constants;
using FlatEarthControlSystem.Extensions;
using FlatEarthControlSystem.WorldDefinition;
using TextAdventureGameInputParser;
using TextAdventureGameInputParser.WordClass;

namespace FlatEarthControlSystem
{
    public class CommandExecuter
    {
        private readonly World _world;
        private readonly Player _player;
        private readonly Room _currentRoom;

        public CommandExecuter(Player player, World world, Room currentRoom)
        {
            _player = player;
            _world = world;
            _currentRoom = currentRoom;
        }

        public CommandResult Apply(Sentence sentence)
        {
            if (sentence.Word1Is(Words.Exits))
                return GetExits();

            if (sentence.Word1Is(Words.Go))
                return sentence.Word4IsEmpty()
                    ? Fail(StandardAnswers.GoWhere)
                    : Go(sentence.Word4!);

            if (sentence.Word1Is(Words.Inventory))
                return Inventory();

            if (sentence.Word1Is(Words.Look))
                return Look();

            throw new ArgumentOutOfRangeException(@"Unknown word found.");
        }

        public CommandResult Go(Noun direction)
        {
            var exit = _currentRoom.GetDiscoveredExit(direction);
            if (exit == null)
                return Fail(StandardAnswers.YouCantGoThatWay);

            //TODO: Check conditions.

            var room = _player.SetCurrentRoomId(exit.TargetRoomId, _world);
            var roomDescription = room.GetDescription();

            return Success(
                roomDescription.IsEmpty()
                    ? StandardAnswers.Ok
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
            new(false, text);

        private static CommandResult Success(string text) =>
            new(true, text);
    }
}
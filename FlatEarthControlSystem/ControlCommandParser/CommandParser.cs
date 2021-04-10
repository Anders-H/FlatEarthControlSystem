using FlatEarthControlSystem.ControlCommandParser.WordTypes.Nouns;
using FlatEarthControlSystem.PreProcessing;
using FlatEarthControlSystem.WorldDefinition;
using MutableStringLibrary;

namespace FlatEarthControlSystem.ControlCommandParser
{
    public class CommandParser
    {
        private readonly World _world;
        private readonly Room _currentRoom;
        private readonly Inventory _inventory;

        public CommandParser(World world, Room currentRoom, Inventory inventory)
        {
            _world = world;
            _currentRoom = currentRoom;
            _inventory = inventory;
        }

        public CommandParserResult Parse(string commandString)
        {
            var s = new MutableString(commandString);
            s.Modify.MiddleTrim();
            commandString = s.Value!.OnlyBasicCharacters();

            if (string.IsNullOrWhiteSpace(commandString))
                return CommandParserResult.CreateEmptyResult();

            if (commandString.IsSingleWord())
                return ParseSingleWordCommand(commandString);

            return null;

        }

        private CommandParserResult ParseSingleWordCommand(string s)
        {
            var direction = Direction.FromNoun(s);
            if (direction != null)
                return CommandParserResult.CreateSuccessResult(
                    SuggestedCommand.Go(direction),
                    PreProcessorIntention.Move
                );

            var command = SingleAction.FromNoun(s);
            if (command != null)
                return CommandParserResult.CreateSuccessResult(
                    command,
                    PreProcessorIntention.Empty //TODO!
                );

            return null;
            
        }
    }
}
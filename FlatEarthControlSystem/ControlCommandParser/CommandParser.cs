using System.Collections.Generic;
using System.Linq;
using FlatEarthControlSystem.PreProcessing;
using FlatEarthControlSystem.WorldDefinition;

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
            var singleCommandParserResult = GetSingleCommandParserResult(commandString);
            if (singleCommandParserResult != null && singleCommandParserResult.Success)
                return singleCommandParserResult;

            return null; //TODO!!!!
        }

        private CommandParserResult? GetSingleCommandParserResult(string commandString)
        {
            var singleCommandRecognizer = new SingleCommandRecognizer(commandString);
            if (singleCommandRecognizer.IsLook())
                return CommandParserResult.CreateSuccessResult(
                    SuggestedCommand.Look(),
                    PreProcessorIntention.Look
                );
            if (singleCommandRecognizer.IsGoNorth())
                return CommandParserResult.CreateSuccessResult(
                    SuggestedCommand.GoNorth(),
                    PreProcessorIntention.Move
                );



            return null; //TODO!!!!
        }
    }
}
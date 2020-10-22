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
            commandString = commandString
                .MiddleTrim()
                .OnlyBasicCharacters();

            if (string.IsNullOrWhiteSpace(commandString))
                return CommandParserResult.CreateEmptyResult();



        }
    }
}
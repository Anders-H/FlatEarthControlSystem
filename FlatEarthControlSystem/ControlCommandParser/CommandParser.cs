using System.Collections.Generic;
using System.Linq;
using FlatEarthControlSystem.WorldDefinition;

namespace FlatEarthControlSystem.ControlCommandParser
{
    public class CommandParser
    {
        private readonly Room _room;
        private readonly string _phrase;
        private List<string> _parts;
        
        public CommandParser(Room room, string phrase)
        {
            _room = room;
            _phrase = phrase.MiddleTrim();
            _parts = _phrase.Split(' ').ToList();
        }

        public CommandParserResult Parse()
        {
            if (string.IsNullOrWhiteSpace(_phrase) || _parts.Count <= 0)
                return CommandParserResult.CreateFailResult("");
            var command = _parts.Pop();
            switch (command)
            {
                case "INVENTORY":
                case "INV":
                    return CommandParserResult.CreateSuccessResult(new Command(Intention.Inventory, "SHOW", "INVENTORY"));
                case "N":
                case "NORTH":
                    return CommandParserResult.CreateSuccessResult(new Command(Intention.Move, "GO", "NORTH"));
                case "E":
                case "EAST":
                    return CommandParserResult.CreateSuccessResult(new Command(Intention.Move, "GO", "EAST"));
                case "S":
                case "SOUTH":
                    return CommandParserResult.CreateSuccessResult(new Command(Intention.Move, "GO", "SOUTH"));
                case "W":
                case "WEST":
                    return CommandParserResult.CreateSuccessResult(new Command(Intention.Move, "GO", "SOUTH"));
                case "EXITS":
                    return CommandParserResult.CreateSuccessResult(new Command(Intention.Exits, "SHOW", "EXITS"));
            }
            if (_parts.Count <= 0)
                return CommandParserResult.CreateFailResult();
            switch (command)
            {
                case "GO":
                    return Go();
                case "SHOW":
                    return Show();
                default:
                    return CommandParserResult.CreateFailResult();
            }
        }

        private CommandParserResult Go()
        {
            if (_parts.Count != 1)
                return CommandParserResult.CreateFailResult();
            var direction = _parts[0];
            var exit = _room.GetDiscoveredExit(direction);
            return exit == null
                ? CommandParserResult.CreateFailResult("CAN'T GO THAT WAY.")
                : CommandParserResult.CreateSuccessResult(new Command(Intention.Move, "GO", exit.ToString()));
        }

        private CommandParserResult Show()
        {
            const string error = "SHOW WHAT?";
            var show = _parts.Pop();
            if (string.IsNullOrWhiteSpace(show))
                return CommandParserResult.CreateFailResult(error);
            switch (show)
            {
                case "INVENTORY":
                case "INV":
                    return CommandParserResult.CreateSuccessResult(new Command(Intention.Inventory, "SHOW", "INVENTORY"));
                case "EXITS":
                    return CommandParserResult.CreateSuccessResult(new Command(Intention.Exits, "SHOW", "EXITS"));
            }
            return CommandParserResult.CreateFailResult(error);
        }
    }
}
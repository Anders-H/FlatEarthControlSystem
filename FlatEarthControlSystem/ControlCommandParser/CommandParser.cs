using System.Collections.Generic;
using System.Linq;
using FlatEarthControlSystem.PreProcessing;
using FlatEarthControlSystem.WorldDefinition;

namespace FlatEarthControlSystem.ControlCommandParser
{
    public class CommandParser
    {
        private readonly Room _room;
        private readonly string _phrase;
        private readonly List<string> _parts;
        
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
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand("SHOW", "INVENTORY"),
                        PreProcessorIntention.Inventory);
                case "N":
                case "NORTH":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand("GO", "NORTH"),
                        PreProcessorIntention.Move);
                case "E":
                case "EAST":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand("GO", "EAST"),
                        PreProcessorIntention.Move);
                case "S":
                case "SOUTH":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand("GO", "SOUTH"),
                        PreProcessorIntention.Move);
                case "W":
                case "WEST":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand("GO", "SOUTH"),
                        PreProcessorIntention.Move);
                case "EXITS":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand("SHOW", "EXITS"),
                        PreProcessorIntention.Exits);
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
                : CommandParserResult.CreateSuccessResult(
                    new SuggestedCommand("GO", exit.ToString()),
                    PreProcessorIntention.Move);
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
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand("SHOW", "INVENTORY"),
                        PreProcessorIntention.Inventory);
                case "EXITS":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand("SHOW", "EXITS"),
                        PreProcessorIntention.Exits);
            }
            return CommandParserResult.CreateFailResult(error);
        }
    }
}
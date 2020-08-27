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

        private const string CommandShow = "SHOW";
        private const string SpecialNounInventory = "INVENTORY";
        private const string SpecialNounExits = "EXITS";
        private const string CommandGo = "GO";

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

            //Check if shortcut or single word command is used.
            switch (command)
            {
                case SpecialNounInventory:
                case "INV":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand(CommandShow, "INVENTORY"),
                        PreProcessorIntention.Inventory,
                        CommandShow, SpecialNounInventory, "");
                case "N":
                case "NORTH":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand(CommandGo, "NORTH"),
                        PreProcessorIntention.Move,
                        CommandGo, "NORTH", "");
                case "E":
                case "EAST":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand(CommandGo, "EAST"),
                        PreProcessorIntention.Move,
                        CommandGo, "EAST", "");
                case "S":
                case "SOUTH":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand(CommandGo, "SOUTH"),
                        PreProcessorIntention.Move,
                        CommandGo, "SOUTH", "");
                case "W":
                case "WEST":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand(CommandGo, "WEST"),
                        PreProcessorIntention.Move,
                        CommandGo, "WEST", "");
                case SpecialNounExits:
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand(CommandShow, SpecialNounExits),
                        PreProcessorIntention.Exits,
                        CommandShow, SpecialNounExits, "");
            }

            switch (_parts.Count)
            {
                case 0:
                    return CommandParserResult.CreateFailResult();
                case 1:
                    switch (command)
                    {
                        case CommandGo:
                            return Go();
                        case CommandShow:
                            return Show();
                        default:
                            return CommandParserResult.CreateFailResult();
                    }
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
                    new SuggestedCommand(CommandGo, exit.ToString()),
                    PreProcessorIntention.Move,
                    CommandGo, direction, "");
        }

        private CommandParserResult Show()
        {
            const string error = "SHOW WHAT?";
            var show = _parts.Pop();
            if (string.IsNullOrWhiteSpace(show))
                return CommandParserResult.CreateFailResult(error);
            switch (show)
            {
                case SpecialNounInventory:
                case "INV":
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand(CommandShow, SpecialNounInventory),
                        PreProcessorIntention.Inventory,
                        CommandShow, SpecialNounInventory, "");
                case SpecialNounExits:
                    return CommandParserResult.CreateSuccessResult(
                        new SuggestedCommand(CommandShow, SpecialNounExits),
                        PreProcessorIntention.Exits,
                        CommandShow, SpecialNounExits, "");
            }
            return CommandParserResult.CreateFailResult(error);
        }
    }
}
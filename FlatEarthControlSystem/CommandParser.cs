using FlatEarthControlSystem.WorldDefinition;
using TextAdventureGameInputParser;
using TextAdventureGameInputParser.WordClass;

namespace FlatEarthControlSystem
{
    public class CommandParser
    {
        private static Parser Parser { get; }

        static CommandParser()
        {
            Parser = new Parser();

            Parser.AddVerbs(
                Words.Exits,
                Words.Go,
                Words.Inventory,
                Words.Look
            );

            Parser.Aliases.Add(Words.Inventory, "I", "INV");
        }

        private readonly World _world;
        private readonly Room _currentRoom;
        private readonly Inventory _inventory;

        public CommandParser(World world, Room currentRoom, Inventory inventory)
        {
            _world = world;
            _currentRoom = currentRoom;
            _inventory = inventory;
        }

        public SentenceWrapper Parse(string command)
        {
            var sentence = Parser.Parse(command);
            var result = new SentenceWrapper
            {
                Sentence = sentence
            };

            if (sentence.ParseSuccess)
            {
                if (!sentence.Ambiguous)
                {
                    result.Success = true;
                    result.Message = StandardAnswers.Ok;
                    return result;
                }

                result.Success = false;
                result.Message = sentence.UnknownWord;
            }
            else
            {
                result.Success = false;
                result.Message = StandardAnswers.IdontUnderstand;
            }

            return result;
        }
    }
}
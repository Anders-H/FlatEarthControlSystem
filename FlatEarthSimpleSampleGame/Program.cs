using System;
using System.IO;
using FlatEarthControlSystem;

namespace FlatEarthSimpleSampleGame
{
    public class Program
    {
        public static void Main()
        {
            var consumerUtils = new ConsumerUtils();
            var worldDefinitionPath = Path.Combine(consumerUtils.GetHostDirectory(), @"world.txt");
            var flatEarth = new FlatEarth();

            using (var sr = new StreamReader(worldDefinitionPath))
            {
                var source = sr.ReadToEnd();
                sr.Close();
                flatEarth.Load(source);
            }

            do
            {
                Console.Write("> ");
                var input = (Console.ReadLine() ?? "").Trim();
                if (string.Compare(input, "exit", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    if (Exit())
                        return;
                    continue;
                }

                var room = flatEarth.GetCurrentRoom();

                //room.VisitCount

            } while (true);
        }

        private static bool Exit()
        {
            Console.Write("Exit? (Y/N) ");
            return (Console.ReadLine() ?? "").Trim().ToLower().StartsWith("y");

        }
    }
}
# FlatEarthControlSystem

**FlatEarthControlSystem** is an engine for text-based adventure games. This is an example of a basic game loop:

```
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
            var flatEarth = new FlatEarth();
            flatEarth.Load(...insert game definition here...);

            Console.WriteLine(flatEarth.GetCurrentRoom().GetDescription());

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

                var result = flatEarth.Do(input);
                Console.WriteLine(result.Text);

            } while (true);
        }

        private static bool Exit()
        {
            Console.Write("Exit? (Y/N) ");
            return (Console.ReadLine() ?? "").Trim().ToLower().StartsWith("y");
        }
    }
}
```

The `Load` function of the `FlatEarth` class takes a string that contains the game definition. The string can be loaded from a file, like so:


```
using (var sr = new StreamReader(Path.Combine(consumerUtils.GetHostDirectory(), @"world.txt")))
{
    var source = sr.ReadToEnd();
    sr.Close();
    flatEarth.Load(source);
}
```

Or hard coded in the source code:

```
flatEarth.Load(@"

    ...insert game definition here...

");
```

## Game definition language

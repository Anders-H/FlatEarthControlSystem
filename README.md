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

## World definition language

The World definition language defines the rooms and the objects of the game.
It consists individual lines with zero to one parameter.
Some define blocks and some do not. Some are root level lines, some must reside in certain blocks.

### Room definition

Rooms are defined in blocks at root level.

```
BEGIN ROOM [room ID]

...

END ROOM
```

### Room texts

Four different descriptions can be provided, all are optional. A text that is displayed at the first visit, a text that is displayed at any other visits to a room, a text that is displayed the first time the user looks and a text that is displayed any other time the user looks. These lines must occur in a `ROOM` block.

```
FIRST DESCRIPTION: Text...
DESCRIPTION: Text...
FIRST LOOK: Text...
LOOK: Text...
```

`FIRST DESCRIPTION` and `DESCRIPTION` can fall back to each other, just as `FIRST LOOK` and `LOOK`. Also, if both `FIRST LOOK` and `LOOK` is ommited, it will fall back to the room description.

### Exits

Exits are defined inside of a `ROOM` block. `direction` is the name of the exit and `target room ID` is a room that exits in the world definition.

```
BEGIN EXIT [direction]: [target room ID]
…
END EXIT
```

If an exit should be invisible and unusable, place `NOT DISCOVERED` between `BEGIN EXIT` and `END EXIT`.

### Starting room

World definitions must include a starting room. The ID of an existing room is required.

```
CURRENT ROOM [room ID]
```

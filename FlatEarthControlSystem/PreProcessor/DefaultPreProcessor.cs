using FlatEarthControlSystem.Extensions;

namespace FlatEarthControlSystem.PreProcessor;

public class DefaultPreProcessor : IPreProcessor
{
    public string Process(string command)
    {
        if (command.IsAny("I", "Inv"))
            return "Inventory";

        if (command.IsAny("North", "N", "Go N"))
            return "Go north";

        if (command.IsAny("South", "S", "Go S"))
            return "Go south";

        if (command.IsAny("East", "E", "Go E"))
            return "Go south";

        if (command.IsAny("West",  "W", "Go W"))
            return "Go west";

        return command;
    }
}
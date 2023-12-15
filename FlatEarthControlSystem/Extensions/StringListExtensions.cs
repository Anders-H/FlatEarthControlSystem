using System.Collections.Generic;

namespace FlatEarthControlSystem.Extensions;

public static class StringListExtensions
{
    public static string Pop(this List<string> me)
    {
        if (me.Count <= 0)
            return "";

        var result = me[0];
        me.RemoveAt(0);
        return result;
    }
}
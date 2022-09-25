using System;
using MutableStringLibrary;

namespace FlatEarthControlSystem.Extensions;

public static class StringExtensions
{
    public static string OnlyBasicCharacters(this string me)
    {
        var mutable = new MutableString(me);
        mutable.Modify.LimitToCharacters("ABCDEFGHIJKLMNOPQRSTUVWXYZ 0123456789");
        return mutable.Value ?? "";
    }

    public static bool IsSingleWord(this string me) =>
        !string.IsNullOrWhiteSpace(me) && me.IndexOf(' ') < 0;

    public static bool Is(this string? me, string? other) =>
        string.Compare(me, other, StringComparison.CurrentCultureIgnoreCase) == 0;

    public static bool IsEmpty(this string? me) =>
        string.IsNullOrWhiteSpace(me);
}
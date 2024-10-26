using System;
using System.IO;
using System.Reflection;

namespace FlatEarthControlSystem;

public class ConsumerUtils
{
    public string GetHostPath() =>
        new FileInfo(
            Uri.UnescapeDataString(
                new UriBuilder(Assembly.GetExecutingAssembly().Location).Path
            )
        ).FullName;

    public string GetHostDirectory() =>
        new FileInfo(GetHostPath()).Directory?.FullName ?? "";
}
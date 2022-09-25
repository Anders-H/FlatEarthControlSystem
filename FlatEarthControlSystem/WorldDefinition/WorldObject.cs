namespace FlatEarthControlSystem.WorldDefinition;

public class WorldObject : Referenceable
{
    public bool Discovered { get; set; }
    public bool WillDisposeAfterUse { get; }
    public bool Disposed { get; set; }
    public WorldObjectLocation Location { get; set; }


    public WorldObject(
        string uniqueName,
        string indefiniteArticleForUniqueName,
        string relaxedName,
        string indefiniteArticleForRelaxedName,
        bool discovered,
        bool willDisposeAfterUse
    ) : base(uniqueName, indefiniteArticleForUniqueName, relaxedName, indefiniteArticleForRelaxedName)
    {
        Discovered = discovered;
        WillDisposeAfterUse = willDisposeAfterUse;
        Disposed = false;
        Location = WorldObjectLocation.Disposed;
    }

    public WorldObject(
        string uniqueName,
        string indefiniteArticleForUniqueName,
        string relaxedName,
        string indefiniteArticleForRelaxedName,
        bool discovered
    ) : base(uniqueName, indefiniteArticleForUniqueName, relaxedName, indefiniteArticleForRelaxedName)
    {
        Discovered = discovered;
        WillDisposeAfterUse = false;
        Disposed = false;
        Location = WorldObjectLocation.Disposed;
    }
}
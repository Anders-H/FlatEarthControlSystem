namespace FlatEarthControlSystem.WorldDefinition
{
    public class WorldObject
    {
        public string UniqueName { get; }
        public string RelaxedName { get; }
        public bool Discovered { get; set; }
        public bool WillDisposeAfterUse { get; }
        public bool Disposed { get; set; }
        public string IndefiniteArticle { get; }
        public WorldObjectLocation Location { get; set; }


        public WorldObject(string uniqueName, string relaxedName, bool discovered, bool willDisposeAfterUse, string indefiniteArticle)
        {
            UniqueName = uniqueName;
            RelaxedName = relaxedName;
            Discovered = discovered;
            WillDisposeAfterUse = willDisposeAfterUse;
            Disposed = false;
            IndefiniteArticle = indefiniteArticle;
            Location = WorldObjectLocation.Disposed;
        }

        public WorldObject(string uniqueName, string relaxedName, bool discovered, string indefiniteArticle)
        {
            UniqueName = uniqueName;
            RelaxedName = relaxedName;
            Discovered = discovered;
            WillDisposeAfterUse = false;
            Disposed = false;
            IndefiniteArticle = indefiniteArticle;
            Location = WorldObjectLocation.Disposed;
        }
    }
}
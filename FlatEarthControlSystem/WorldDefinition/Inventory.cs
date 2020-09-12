namespace FlatEarthControlSystem.WorldDefinition
{
    public class Inventory
    {
        private readonly FlatEarth _flatEarth;
        private readonly WorldObjectList _objects;

        public Inventory(FlatEarth flatEarth)
        {
            _flatEarth = new FlatEarth();
            _objects = new WorldObjectList();
        }

        public bool Empty() =>
            _objects.Count <= 0;

        public string EnumerationText =>
            _objects.EnumerationText;

        public void AddWorldObject(string uniqueName, string relaxedName, bool discovered, bool willDisposeAfterUse, string indefiniteArticle) =>
            _objects.Add(new WorldObject(uniqueName, relaxedName, discovered, willDisposeAfterUse, indefiniteArticle));

        public void AddWorldObject(string uniqueName, string relaxedName, bool discovered, string indefiniteArticle) =>
            _objects.Add(new WorldObject(uniqueName, relaxedName, discovered, indefiniteArticle));

        public void AddWorldObject(WorldObject worldObject) =>
            _objects.Add(worldObject);
    }
}
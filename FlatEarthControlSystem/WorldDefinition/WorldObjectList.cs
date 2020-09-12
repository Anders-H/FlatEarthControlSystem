namespace FlatEarthControlSystem.WorldDefinition
{
    public class WorldObjectList : ReferenceableList<WorldObject>
    {
        public void AddWorldObject(string uniqueName, string relaxedName, bool discovered, bool willDisposeAfterUse, string indefiniteArticle) =>
            Add(new WorldObject(uniqueName, relaxedName, discovered, willDisposeAfterUse, indefiniteArticle));

        public void AddWorldObject(string uniqueName, string relaxedName, bool discovered, string indefiniteArticle) =>
            Add(new WorldObject(uniqueName, relaxedName, discovered, indefiniteArticle));

        public void AddWorldObject(WorldObject worldObject) =>
            Add(worldObject);
    }
}
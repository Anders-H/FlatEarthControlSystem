namespace FlatEarthControlSystem.WorldDefinition
{
    public class WorldObjectList : ReferenceableList<WorldObject>
    {
        public void AddWorldObject(string uniqueName, string indefiniteArticleForUniqueName, string relaxedName, string indefiniteArticleForRelaxedName, bool discovered, bool willDisposeAfterUse) =>
            Add(new WorldObject(uniqueName, indefiniteArticleForUniqueName, relaxedName, indefiniteArticleForRelaxedName, discovered, willDisposeAfterUse));

        public void AddWorldObject(string uniqueName, string indefiniteArticleForUniqueName, string relaxedName, string indefiniteArticleForRelaxedName, bool discovered, string indefiniteArticle) =>
            Add(new WorldObject(uniqueName, indefiniteArticleForUniqueName, relaxedName, indefiniteArticleForRelaxedName, discovered));

        public void AddWorldObject(WorldObject worldObject) =>
            Add(worldObject);
    }
}
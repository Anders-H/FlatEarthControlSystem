using FlatEarthControlSystem.Constants;

namespace FlatEarthControlSystem.WorldDefinition
{
    public class Inventory
    {
        private readonly FlatEarth _flatEarth;
        private readonly WorldObjectList _objects;

        public Inventory(FlatEarth flatEarth)
        {
            _flatEarth = flatEarth;
            _objects = new WorldObjectList();
        }

        public bool Empty() =>
            _objects.Count <= 0;

        public string EnumerationText =>
            _objects.Count > 0
                ? $"{StandardAnswers.YouAreCarrying} {_objects.EnumerationText}."
                : StandardAnswers.YouAreNotCarryingAnything;

        public void AddWorldObject(string uniqueName, string indefiniteArticleForUniqueName, string relaxedName, string indefiniteArticleForRelaxedName, bool discovered, bool willDisposeAfterUse) =>
            _objects.Add(new WorldObject(uniqueName, indefiniteArticleForUniqueName, relaxedName, indefiniteArticleForRelaxedName, discovered, willDisposeAfterUse));

        public void AddWorldObject(string uniqueName, string indefiniteArticleForUniqueName, string relaxedName, string indefiniteArticleForRelaxedName, bool discovered) =>
            _objects.Add(new WorldObject(uniqueName, indefiniteArticleForUniqueName, relaxedName, indefiniteArticleForRelaxedName, discovered));

        public void AddWorldObject(WorldObject worldObject) =>
            _objects.Add(worldObject);
    }
}
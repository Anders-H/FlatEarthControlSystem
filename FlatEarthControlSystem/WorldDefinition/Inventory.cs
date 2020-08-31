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
    }
}
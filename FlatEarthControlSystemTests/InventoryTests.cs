using FlatEarthControlSystem;
using FlatEarthControlSystem.WorldDefinition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void CanDescribeEmptyInventory()
        {
            var flatEarth = new FlatEarth();
            var i = new Inventory(flatEarth);
            Assert.IsTrue(i.EnumerationText == "You are no carrying anything.");
        }

        [TestMethod]
        public void CanDescribeInventoryWithOneObject()
        {
            var flatEarth = new FlatEarth();
            var i = new Inventory(flatEarth);

            i.AddWorldObject("fish", "a", "", "", true);
            
            Assert.IsTrue(i.EnumerationText == "You are carrying a fish.");
        }

        [TestMethod]
        public void CanPickCorrectIndefiniteArticle()
        {
            var flatEarth = new FlatEarth();
            var i = new Inventory(flatEarth);
            
            i.AddWorldObject("elf", "an", "dead elf", "a", true);
            i.AddWorldObject("elf", "an", "happy elf", "a", true);

            Assert.IsTrue(i.EnumerationText == "You are carrying a dead elf and a happy elf.");
        }

        [TestMethod]
        public void CanUseShortestAvailableName()
        {
            var flatEarth = new FlatEarth();
            var i1 = new Inventory(flatEarth);

            i1.AddWorldObject("golden key", "a", "key", "a", true);
            i1.AddWorldObject("skeleton key", "a", "key", "a", true);
            i1.AddWorldObject("elf", "an", "dead elf", "a", true);

            Assert.IsTrue(i1.EnumerationText == "You are carrying a golden key, a skeleton key and an elf.");
        }
    }
}
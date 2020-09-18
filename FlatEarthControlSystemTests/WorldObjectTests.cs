using FlatEarthControlSystem.WorldDefinition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class WorldObjectTests
    {
        [TestMethod]
        public void CanIdentifyObjectWithNames()
        {
            var objectWithName1 = new WorldObject("X1", "a", "X2", "a", true);
            var objectWithName2 = new WorldObject("X3", "an", "", "", true);
            var objectWithName3 = new WorldObject("", "", "X4", "a", true);
            var objectWithoutName = new WorldObject("", "", "", "", true);
            Assert.IsTrue(objectWithName1.HasName);
            Assert.IsTrue(objectWithName2.HasName);
            Assert.IsTrue(objectWithName3.HasName);
            Assert.IsFalse(objectWithoutName.HasName);
        }

        [TestMethod]
        public void CanIdentifyMultipleNames()
        {
            var objectWithMultipleNames = new WorldObject("X1", "a", "X2", "a", true);
            var objectWithName1 = new WorldObject("X3", "an", "", "", true);
            var objectWithName2 = new WorldObject("", "", "X4", "a", true);
            var objectWithoutName = new WorldObject("", "", "", "", true);
            Assert.IsTrue(objectWithMultipleNames.ItemHasMultipleNames);
            Assert.IsFalse(objectWithName1.ItemHasMultipleNames);
            Assert.IsFalse(objectWithName2.ItemHasMultipleNames);
            Assert.IsFalse(objectWithoutName.ItemHasMultipleNames);
        }

        [TestMethod]
        public void CanGetMostUniqueName()
        {
            var o1 = new WorldObject("X1", "a", "X2", "a", true);
            var o2 = new WorldObject("X3", "an", "", "", true);
            var o3 = new WorldObject("", "", "X4", "a", true);
            var o4 = new WorldObject("", "", "", "", true);
            Assert.IsTrue(o1.GetMostUniqueName() == "X1");
            Assert.IsTrue(o2.GetMostUniqueName() == "X3");
            Assert.IsTrue(o3.GetMostUniqueName() == "X4");
            Assert.IsTrue(o4.GetMostUniqueName() == "");
        }

        [TestMethod]
        public void CanGetMostRelaxedName()
        {
            var o1 = new WorldObject("X1", "a", "X2", "a", true);
            var o2 = new WorldObject("X3", "an", "", "", true);
            var o3 = new WorldObject("", "", "X4", "a", true);
            var o4 = new WorldObject("", "", "", "", true);
            Assert.IsTrue(o1.GetMostRelaxedName() == "X2");
            Assert.IsTrue(o2.GetMostRelaxedName() == "X3");
            Assert.IsTrue(o3.GetMostRelaxedName() == "X4");
            Assert.IsTrue(o4.GetMostRelaxedName() == "");
        }
    }
}
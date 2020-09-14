using FlatEarthControlSystem.WorldDefinition;
using Xunit;

namespace FlatEarthControlSystemTests
{
    public class WorldObjectTests
    {
        [Fact]
        public void CanIdentifyObjectWithNames()
        {
            var objectWithName1 = new WorldObject("X1", "X2", true, "a");
            var objectWithName2 = new WorldObject("X3", "", true, "an");
            var objectWithName3 = new WorldObject("", "X4", true, "a");
            var objectWithoutName = new WorldObject("", "", true, "an");
            Assert.True(objectWithName1.HasName);
            Assert.True(objectWithName2.HasName);
            Assert.True(objectWithName3.HasName);
            Assert.False(objectWithoutName.HasName);
        }

        [Fact]
        public void CanIdentifyMultipleNames()
        {
            var objectWithMultipleNames = new WorldObject("X1", "X2", true, "a");
            var objectWithName1 = new WorldObject("X3", "", true, "an");
            var objectWithName2 = new WorldObject("", "X4", true, "a");
            var objectWithoutName = new WorldObject("", "", true, "an");
            Assert.True(objectWithMultipleNames.ItemHasMultipleNames);
            Assert.False(objectWithName1.ItemHasMultipleNames);
            Assert.False(objectWithName2.ItemHasMultipleNames);
            Assert.False(objectWithoutName.ItemHasMultipleNames);
        }

        [Fact]
        public void CanGetMostUniqueName()
        {
            var o1 = new WorldObject("X1", "X2", true, "a");
            var o2 = new WorldObject("X3", "", true, "an");
            var o3 = new WorldObject("", "X4", true, "a");
            var o4 = new WorldObject("", "", true, "an");
            Assert.True(o1.GetMostUniqueName() == "X1");
            Assert.True(o2.GetMostUniqueName() == "X3");
            Assert.True(o3.GetMostUniqueName() == "X4");
            Assert.True(o4.GetMostUniqueName() == "");
        }

        [Fact]
        public void CanGetMostRelaxedName()
        {
            var o1 = new WorldObject("X1", "X2", true, "a");
            var o2 = new WorldObject("X3", "", true, "an");
            var o3 = new WorldObject("", "X4", true, "a");
            var o4 = new WorldObject("", "", true, "an");
            Assert.True(o1.GetMostRelaxedName() == "X2");
            Assert.True(o2.GetMostRelaxedName() == "X3");
            Assert.True(o3.GetMostRelaxedName() == "X4");
            Assert.True(o4.GetMostRelaxedName() == "");
        }
    }
}
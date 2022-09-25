using FlatEarthControlSystem;
using FlatEarthControlSystem.Constants;
using FlatEarthControlSystem.WorldDefinition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests;

[TestClass]
public class InventoryTests
{
    [TestMethod]
    public void CanDescribeEmptyInventory()
    {
        var flatEarth = new FlatEarth();
        var i = new Inventory(flatEarth);
        Assert.IsTrue(i.EnumerationText == StandardAnswers.YouAreNotCarryingAnything);
    }

    [TestMethod]
    public void CanDescribeInventoryWithOneObject()
    {
        var flatEarth = new FlatEarth();
        var i = new Inventory(flatEarth);

        i.AddWorldObject("fish", "a", "", "", true);
            
        Assert.IsTrue(i.EnumerationText == $"{StandardAnswers.YouAreCarrying} a fish.");
    }

    [TestMethod]
    public void CanPickCorrectIndefiniteArticle()
    {
        var flatEarth = new FlatEarth();
        var i = new Inventory(flatEarth);
            
        i.AddWorldObject("elf", "an", "dead elf", "a", true);
        i.AddWorldObject("elf", "an", "happy elf", "a", true);

        Assert.IsTrue(i.EnumerationText == $"{StandardAnswers.YouAreCarrying} a dead elf AND a happy elf.");
    }

    [TestMethod]
    public void CanUseShortestAvailableName()
    {
        var flatEarth = new FlatEarth();
        var i1 = new Inventory(flatEarth);

        i1.AddWorldObject("golden key", "a", "key", "a", true);
        i1.AddWorldObject("skeleton key", "a", "key", "a", true);
        i1.AddWorldObject("dead elf", "a", "elf", "an", true);

        Assert.IsTrue(i1.EnumerationText == $"{StandardAnswers.YouAreCarrying} a golden key, a skeleton key AND an elf.");
    }
}
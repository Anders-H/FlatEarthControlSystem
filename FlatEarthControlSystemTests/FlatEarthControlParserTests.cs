using System;
using FlatEarthControlSystem.WorldDefinition;
using FlatEarthControlSystemTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests;

[TestClass]
public class FlatEarthControlParserTests : FlatEarthControlBase
{
    [TestMethod]
    public void CanNavigate()
    {
        var flatEarth = GetCanNavigateTestData();
        Assert.IsTrue(flatEarth.Player.GetCurrentRoomId () == "5,5");
        Assert.IsTrue(flatEarth.Do("GO NORTH")!.Success);
        Assert.IsTrue(flatEarth.Player.GetCurrentRoomId() == "5,4");
        Assert.IsTrue(flatEarth.Do("GO SOUTH")!.Success);
        Assert.IsTrue(flatEarth.Player.GetCurrentRoomId() == "5,5");
        Assert.IsFalse(flatEarth.Do("GO SOUTH")!.Success);
        Assert.IsTrue(flatEarth.Player.GetCurrentRoomId() == "5,5");
    }

    [TestMethod]
    public void CanSeeExits()
    {
        var flatEarth = GetRoomInitialVisibilityTestData();
        flatEarth.GetCurrentRoom()!.GetAnyExit("WEST")!.Discovered = false;

        var response = flatEarth.Do("EXITS");
        Assert.IsTrue(response!.Success);
        Assert.IsTrue(string.Compare(response.Text, "EXITS ARE: NORTH.", StringComparison.CurrentCultureIgnoreCase) == 0);
            
        flatEarth.GetCurrentRoom()!.GetAnyExit("WEST")!.Discovered = true;
        response = flatEarth.Do("EXITS");
        Assert.IsTrue(response!.Success);
        Assert.IsTrue(string.Compare(response.Text, "EXITS ARE: NORTH AND WEST.", StringComparison.CurrentCultureIgnoreCase) == 0);

        flatEarth.GetCurrentRoom().AddExit(new Exit("SOUTH", "4,5"));
        response = flatEarth.Do("EXITS");
        Assert.IsTrue(response!.Success);
        Assert.IsTrue(string.Compare(response.Text, "EXITS ARE: NORTH, WEST AND SOUTH.", StringComparison.CurrentCultureIgnoreCase) == 0);
    }
        
    [TestMethod]
    public void CanAdjustDescriptionResponseFromVisitCount()
    {
        var flatEarth = GetVisitDescriptionTestData();
        Assert.IsTrue(flatEarth.SetCurrentRoomId("1,1") == "1-1");
        Assert.IsTrue(flatEarth.Do("GO SOUTH")!.Text == "2-1");
        Assert.IsTrue(flatEarth.Do("GO NORTH")!.Text == "1-2");
        Assert.IsTrue(flatEarth.Do("GO SOUTH")!.Text == "2-2");
    }
        
    [TestMethod]
    public void CanAdjustLookResponseFromVisitCount()
    {
        var flatEarth = GetLookTestData();
        Assert.IsTrue(flatEarth.Do("look").Text == "1-1");
        Assert.IsTrue(flatEarth.Do("look").Text == "1-2");
        Assert.IsTrue(flatEarth.Do("look").Text == "1-2");
        flatEarth.Do("GO SOUTH");
        Assert.IsTrue(flatEarth.Do("look").Text == "2-1");
        Assert.IsTrue(flatEarth.Do("look").Text == "2-2");
        Assert.IsTrue(flatEarth.Do("look").Text == "2-2");
        flatEarth.Do("GO NORTH");
        Assert.IsTrue(flatEarth.Do("look").Text == "1-1");
        Assert.IsTrue(flatEarth.Do("look").Text == "1-2");
        Assert.IsTrue(flatEarth.Do("look").Text == "1-2");
    }

    [TestMethod]
    public void CanIdentifyWordClasses()
    {
        var flatEarth = GetLookTestData();
        //var cp = new CommandParser(flatEarth.GetCurrentRoom(), "GO NORTH");
        //var cpr = cp.Parse();

    }
}
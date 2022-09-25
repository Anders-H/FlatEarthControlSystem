using FlatEarthControlSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventureGameInputParser.WordClass;

namespace FlatEarthControlSystemTests;

[TestClass]
public class FlatEarthBuilderTests
{
    [TestMethod]
    public void CanDoPaths()
    {
        var flatEarth = new FlatEarth();
        flatEarth.Load(@"
BEGIN ROOM 1,1
BEGIN EXIT SOUTH: 1,2
END EXIT
END ROOM

BEGIN ROOM 1,2
BEGIN EXIT NORTH: 1,1
END EXIT
END ROOM

CURRENT ROOM 1,1
");
        Assert.IsTrue(flatEarth.GetCurrentRoom().Id == "1,1");
        Assert.IsTrue(flatEarth.World.RoomCount == 2);
        Assert.IsTrue(flatEarth.World.GetRoom(0)!.Id == "1,1");
        Assert.IsTrue(flatEarth.World.GetRoom(0)!
            .CanGo(
                new Noun("SOUTH"), 
                out var targetRoomId
            )
        );
        Assert.IsTrue(targetRoomId == "1,2");
        Assert.IsTrue(flatEarth.World.GetRoom(1)!.Id == "1,2");
        Assert.IsTrue(flatEarth.World.GetRoom(1)!
            .CanGo(
                new Noun("NORTH"), 
                out targetRoomId
            )
        );
        Assert.IsTrue(targetRoomId == "1,1");
        Assert.IsFalse(flatEarth.World.GetRoom(1)!
            .CanGo(
                new Noun("SOUTH"), 
                out _
            )
        );
    }
        
    [TestMethod]
    public void RoomInitialVisibility()
    {
        var flatEarth = new FlatEarth();
        flatEarth.Load(@"
BEGIN ROOM 5,5
BEGIN EXIT NORTH: 5,4
END EXIT
BEGIN EXIT WEST: 4,5
NOT DISCOVERED
END EXIT
END ROOM

BEGIN ROOM 5,4
BEGIN EXIT SOUTH: 5,5
END EXIT
END ROOM

BEGIN ROOM 4,5
BEGIN EXIT NORTH: 5,5
END EXIT
END ROOM

CURRENT ROOM 5,5
");
        var currentRoom = flatEarth.GetCurrentRoom();
        Assert.IsTrue(currentRoom.CanGo(new Noun("NORTH"), out var targetId));
        Assert.IsTrue(targetId == "5,4");
        Assert.IsFalse(currentRoom.CanGo(new Noun("WEST"), out targetId));
        Assert.IsTrue(targetId == "4,5");
        Assert.IsFalse(currentRoom.CanGo(new Noun("SOUTH"), out targetId));
        Assert.IsTrue(string.IsNullOrWhiteSpace(targetId));
            
        var exit = currentRoom.GetDiscoveredExit(new Noun("WEST"));
        Assert.IsTrue(exit == null);
        exit = currentRoom.GetAnyExit(new Noun("WEST"));
        Assert.IsFalse(exit == null);
        exit!.Discovered = true;
            
        Assert.IsTrue(currentRoom.CanGo(new Noun("NORTH"), out targetId));
        Assert.IsTrue(targetId == "5,4");
        Assert.IsTrue(currentRoom.CanGo(new Noun("WEST"), out targetId));
        Assert.IsTrue(targetId == "4,5");
        Assert.IsFalse(currentRoom.CanGo(new Noun("SOUTH"), out targetId));
    }
}
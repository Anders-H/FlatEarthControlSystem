using FlatEarthControlSystem;
using FlatEarthControlSystem.ControlCommandParser.WordTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class FlatEarthBuilderTests
    {
        [TestMethod]
        public void WithoutUppercaseDeclarationGameIsMixedCase()
        {
            var flatEarth = new FlatEarth();
            flatEarth.Load(@"
BEGIN ROOM 100,100
DESCRIPTION: You are in a room.
END ROOM
CURRENT ROOM 100,100
");
            Assert.IsTrue(flatEarth.GetCurrentRoom().GetDescription() == "You are in a room.");

            flatEarth.Load(@"
BEGIN ROOM 100,100
DESCRIPTION: You are in a room.
END ROOM
CURRENT ROOM 100,100
UPPERCASE
");
            Assert.IsTrue(flatEarth.GetCurrentRoom().GetDescription() == "YOU ARE IN A ROOM.");
        }

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
                    Noun.South(),
                    out var targetRoomId
                )
            );
            Assert.IsTrue(targetRoomId == "1,2");
            Assert.IsTrue(flatEarth.World.GetRoom(1)!.Id == "1,2");
            Assert.IsTrue(flatEarth.World.GetRoom(1)!
                .CanGo(
                    Noun.North(),
                    out targetRoomId
                )
            );
            Assert.IsTrue(targetRoomId == "1,1");
            Assert.IsFalse(flatEarth.World.GetRoom(1)!
                .CanGo(
                    Noun.South(),
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
            Assert.IsTrue(currentRoom.CanGo(Noun.North(), out var targetId));
            Assert.IsTrue(targetId == "5,4");
            Assert.IsFalse(currentRoom.CanGo(Noun.West(), out targetId));
            Assert.IsTrue(targetId == "4,5");
            Assert.IsFalse(currentRoom.CanGo(Noun.South(), out targetId));
            Assert.IsTrue(string.IsNullOrWhiteSpace(targetId));
            
            var exit = currentRoom.GetDiscoveredExit(Noun.West());
            Assert.IsTrue(exit == null);
            exit = currentRoom.GetAnyExit(Noun.West());
            Assert.IsFalse(exit == null);
            exit!.Discovered = true;
            
            Assert.IsTrue(currentRoom.CanGo(Noun.North(), out targetId));
            Assert.IsTrue(targetId == "5,4");
            Assert.IsTrue(currentRoom.CanGo(Noun.West(), out targetId));
            Assert.IsTrue(targetId == "4,5");
            Assert.IsFalse(currentRoom.CanGo(Noun.South(), out targetId));
        }
    }
}
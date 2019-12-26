using FlatEarthControlSystem;
using FlatEarthControlSystem.ControlCommandParser.Words;
using Xunit;

namespace FlatEarthControlSystemTests
{
    public class FlatEarthBuilderTests
    {
        [Fact]
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
            Assert.True(flatEarth.GetCurrentRoom().Id == "1,1");
            Assert.True(flatEarth.World.RoomCount == 2);
            Assert.True(flatEarth.World.GetRoom(0).Id == "1,1");
            Assert.True(flatEarth.World.GetRoom(0)
                .CanGo(
                    new Noun("SOUTH"),
                    out var targetRoomId
                )
            );
            Assert.True(targetRoomId == "1,2");
            Assert.True(flatEarth.World.GetRoom(1).Id == "1,2");
            Assert.True(flatEarth.World.GetRoom(1)
                .CanGo(
                    new Noun("NORTH"),
                    out targetRoomId
                )
            );
            Assert.True(targetRoomId == "1,1");
            Assert.False(flatEarth.World.GetRoom(1)
                .CanGo(
                    new Noun("SOUTH"),
                    out _
                )
            );
        }
        
        [Fact]
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
            Assert.True(currentRoom.CanGo(new Noun("NORTH"), out var targetId));
            Assert.True(targetId == "5,4");
            Assert.False(currentRoom.CanGo(new Noun("WEST"), out targetId));
            Assert.True(targetId == "4,5");
            Assert.False(currentRoom.CanGo(new Noun("SOUTH"), out targetId));
            Assert.True(string.IsNullOrWhiteSpace(targetId));
            
            var exit = currentRoom.GetDiscoveredExit("WEST");
            Assert.True(exit == null);
            exit = currentRoom.GetAnyExit("WEST");
            exit.Discovered = true;
            
            Assert.True(currentRoom.CanGo(new Noun("NORTH"), out targetId));
            Assert.True(targetId == "5,4");
            Assert.True(currentRoom.CanGo(new Noun("WEST"), out targetId));
            Assert.True(targetId == "4,5");
            Assert.False(currentRoom.CanGo(new Noun("SOUTH"), out targetId));
        }
    }
}
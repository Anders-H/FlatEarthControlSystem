using FlatEarthControlSystem;
using FlatEarthControlSystem.ControlCommandParser.Words;
using FlatEarthControlSystem.WorldDefinition;
using FlatEarthControlSystemTests.Common;
using Xunit;

namespace FlatEarthControlSystemTests
{
    public class FlatEarthControlTests : FlatEarchControlBase
    {
        [Fact]
        public void CanNavigate()
        {
            var flatEarth = GetCanNavigateTestData();
            Assert.True(flatEarth.Player.GetCurrentRoomId() == "5,5");
            Assert.True(flatEarth.Go(new Noun("NORTH")).Success);
            Assert.True(flatEarth.Player.GetCurrentRoomId() == "5,4");
            Assert.True(flatEarth.Go(new Noun("SOUTH")).Success);
            Assert.True(flatEarth.Player.GetCurrentRoomId() == "5,5");
            Assert.False(flatEarth.Go(new Noun("SOUTH")).Success);
            Assert.True(flatEarth.Player.GetCurrentRoomId() == "5,5");
        }

        [Fact]
        public void RoomInitialVisibility()
        {
            var flatEarth = GetRoomInitialVisibilityTestData();
            var currentRoom = flatEarth.GetCurrentRoom();
            Assert.True(currentRoom.CanGo(new Noun("NORTH"), out var targetId));
            Assert.True(targetId == "5,4");
            Assert.False(currentRoom.CanGo(new Noun("WEST"), out targetId));
            Assert.True(targetId == "4,5");
            Assert.False(currentRoom.CanGo(new Noun("SOUTH"), out targetId));
            Assert.True(string.IsNullOrWhiteSpace(targetId));
            
            var exit = currentRoom.GetAnyExit("WEST");
            Assert.True(exit != null);
            exit.Discovered = true;
            
            Assert.True(currentRoom.CanGo(new Noun("NORTH"), out targetId));
            Assert.True(targetId == "5,4");
            Assert.True(currentRoom.CanGo(new Noun("WEST"), out targetId));
            Assert.True(targetId == "4,5");
            Assert.False(currentRoom.CanGo(new Noun("SOUTH"), out targetId));
        }
        
        [Fact]
        public void CanSeeExits()
        {
            var flatEarth = GetRoomInitialVisibilityTestData();

            var response = flatEarth.GetExits();
            Assert.True(response.Success);
            Assert.True(response.Text == "EXITS ARE: NORTH.");
            
            flatEarth.GetCurrentRoom().GetAnyExit("WEST").Discovered = true;
            response = flatEarth.GetExits();
            Assert.True(response.Success);
            Assert.True(response.Text == "EXITS ARE: NORTH AND WEST.");

            flatEarth.GetCurrentRoom().AddExit(new Exit("SOUTH", "5,4"));
            response = flatEarth.GetExits();
            Assert.True(response.Success);
            Assert.True(response.Text == "EXITS ARE: NORTH, WEST AND SOUTH.");

            flatEarth.GetCurrentRoom().AddExit(new Exit("EAST", "4,5"));
            response = flatEarth.GetExits();
            Assert.True(response.Success);
            Assert.True(response.Text == "EXITS ARE: NORTH, WEST, SOUTH AND EAST.");
        }

        [Fact]
        public void CanAdjustDescriptionResponseFromVisitCount()
        {
            var flatEarth = GetVisitDescriptionTestData();
            Assert.True(flatEarth.SetCurrentRoomId("1,1") == "1-1");
            Assert.True(flatEarth.Go(new Noun("SOUTH")).Text == "2-1");
            Assert.True(flatEarth.Go(new Noun("NORTH")).Text == "1-2");
            Assert.True(flatEarth.Go(new Noun("SOUTH")).Text == "2-2");
        }
        
        [Fact]
        public void CanAdjustLookResponseFromVisitCount()
        {
            var flatEarth = GetLookTestData();
            Assert.True(flatEarth.Look().Text == "1-1");
            Assert.True(flatEarth.Look().Text == "1-2");
            Assert.True(flatEarth.Look().Text == "1-2");
            flatEarth.Go(new Noun("SOUTH"));
            Assert.True(flatEarth.Look().Text == "2-1");
            Assert.True(flatEarth.Look().Text == "2-2");
            Assert.True(flatEarth.Look().Text == "2-2");
            flatEarth.Go(new Noun("NORTH"));
            Assert.True(flatEarth.Look().Text == "1-1");
            Assert.True(flatEarth.Look().Text == "1-2");
            Assert.True(flatEarth.Look().Text == "1-2");
        }
    }
}
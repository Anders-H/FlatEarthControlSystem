using FlatEarthControlSystem.WorldDefinition;
using FlatEarthControlSystemTests.Common;
using Xunit;

namespace FlatEarthControlSystemTests
{
    public class FlatEarthControlParserTests : FlatEarchControlBase
    {
        [Fact]
        public void CanNavigate()
        {
            var flatEarth = GetCanNavigateTestData();
            Assert.True(flatEarth.Player.GetCurrentRoomId () == "5,5");
            Assert.True(flatEarth.Do("GO NORTH").Success);
            Assert.True(flatEarth.Player.GetCurrentRoomId() == "5,4");
            Assert.True(flatEarth.Do("GO SOUTH").Success);
            Assert.True(flatEarth.Player.GetCurrentRoomId() == "5,5");
            Assert.False(flatEarth.Do("GO SOUTH").Success);
            Assert.True(flatEarth.Player.GetCurrentRoomId() == "5,5");
        }

        [Fact]
        public void CanSeeExits()
        {
            var flatEarth = GetRoomInitialVisibilityTestData();
            flatEarth.GetCurrentRoom().GetAnyExit("WEST").Discovered = false;

            var response = flatEarth.Do("EXITS");
            Assert.True(response.Success);
            Assert.True(response.Text == "EXITS ARE: NORTH.");
            
            flatEarth.GetCurrentRoom().GetAnyExit("WEST").Discovered = true;
            response = flatEarth.Do("EXITS");
            Assert.True(response.Success);
            Assert.True(response.Text == "EXITS ARE: NORTH AND WEST.");

            flatEarth.GetCurrentRoom().AddExit(new Exit("SOUTH", "4,5"));
            response = flatEarth.Do("EXITS");
            Assert.True(response.Success);
            Assert.True(response.Text == "EXITS ARE: NORTH, WEST AND SOUTH.");
        }
    }
}
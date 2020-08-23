using System;
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
            Assert.True(flatEarth.Do("GO NORTH")!.Success);
            Assert.True(flatEarth.Player.GetCurrentRoomId() == "5,4");
            Assert.True(flatEarth.Do("GO SOUTH")!.Success);
            Assert.True(flatEarth.Player.GetCurrentRoomId() == "5,5");
            Assert.False(flatEarth.Do("GO SOUTH")!.Success);
            Assert.True(flatEarth.Player.GetCurrentRoomId() == "5,5");
        }

        [Fact]
        public void CanSeeExits()
        {
            var flatEarth = GetRoomInitialVisibilityTestData();
            flatEarth.GetCurrentRoom().GetAnyExit("WEST").Discovered = false;

            var response = flatEarth.Do("EXITS");
            Assert.True(response!.Success);
            Assert.True(string.Compare(response.Text, "EXITS ARE: NORTH.", StringComparison.CurrentCultureIgnoreCase) == 0);
            
            flatEarth.GetCurrentRoom().GetAnyExit("WEST").Discovered = true;
            response = flatEarth.Do("EXITS");
            Assert.True(response!.Success);
            Assert.True(string.Compare(response.Text, "EXITS ARE: NORTH AND WEST.", StringComparison.CurrentCultureIgnoreCase) == 0);

            flatEarth.GetCurrentRoom().AddExit(new Exit("SOUTH", "4,5"));
            response = flatEarth.Do("EXITS");
            Assert.True(response!.Success);
            Assert.True(string.Compare(response.Text, "EXITS ARE: NORTH, WEST AND SOUTH.", StringComparison.CurrentCultureIgnoreCase) == 0);
        }
        
        [Fact]
        public void CanAdjustDescriptionResponseFromVisitCount()
        {
            var flatEarth = GetVisitDescriptionTestData();
            Assert.True(flatEarth.SetCurrentRoomId("1,1") == "1-1");
            Assert.True(flatEarth.Do("GO SOUTH")!.Text == "2-1");
            Assert.True(flatEarth.Do("GO NORTH")!.Text == "1-2");
            Assert.True(flatEarth.Do("GO SOUTH")!.Text == "2-2");
        }
        
        [Fact]
        public void CanAdjustLookResponseFromVisitCount()
        {
            var flatEarth = GetLookTestData();
            Assert.True(flatEarth.Look().Text == "1-1");
            Assert.True(flatEarth.Look().Text == "1-2");
            Assert.True(flatEarth.Look().Text == "1-2");
            flatEarth.Do("GO SOUTH");
            Assert.True(flatEarth.Look().Text == "2-1");
            Assert.True(flatEarth.Look().Text == "2-2");
            Assert.True(flatEarth.Look().Text == "2-2");
            flatEarth.Do("GO NORTH");
            Assert.True(flatEarth.Look().Text == "1-1");
            Assert.True(flatEarth.Look().Text == "1-2");
            Assert.True(flatEarth.Look().Text == "1-2");
        }
    }
}
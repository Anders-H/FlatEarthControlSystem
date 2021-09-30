using System;
using FlatEarthControlSystem.WorldDefinition;
using FlatEarthControlSystemTests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventureGameInputParser.WordClass;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class FlatEarthControlTests : FlatEarchControlBase
    {
        [TestMethod]
        public void CanNavigate()
        {
            var flatEarth = GetCanNavigateTestData();
            Assert.IsTrue(flatEarth.Player.GetCurrentRoomId() == "5,5");
            Assert.IsTrue(flatEarth.Do("go north").Success);
            Assert.IsTrue(flatEarth.Player.GetCurrentRoomId() == "5,4");
            Assert.IsTrue(flatEarth.Do("go south").Success);
            Assert.IsTrue(flatEarth.Player.GetCurrentRoomId() == "5,5");
            Assert.IsFalse(flatEarth.Do("go south").Success);
            Assert.IsTrue(flatEarth.Player.GetCurrentRoomId() == "5,5");
        }

        [TestMethod]
        public void RoomInitialVisibility()
        {
            var flatEarth = GetRoomInitialVisibilityTestData();
            var currentRoom = flatEarth.GetCurrentRoom();
            Assert.IsTrue(currentRoom.CanGo(Noun.North(), out var targetId));
            Assert.IsTrue(targetId == "5,4");
            Assert.IsFalse(currentRoom.CanGo(Noun.West(), out targetId));
            Assert.IsTrue(targetId == "4,5");
            Assert.IsFalse(currentRoom.CanGo(Noun.North(), out targetId));
            Assert.IsTrue(string.IsNullOrWhiteSpace(targetId));
            
            var exit = currentRoom.GetAnyExit("WEST");
            Assert.IsTrue(exit != null);
            exit!.Discovered = true;
            
            Assert.IsTrue(currentRoom.CanGo(new Noun("NORTH"), out targetId));
            Assert.IsTrue(targetId == "5,4");
            Assert.IsTrue(currentRoom.CanGo(new Noun("WEST"), out targetId));
            Assert.IsTrue(targetId == "4,5");
            Assert.IsFalse(currentRoom.CanGo(new Noun("SOUTH"), out targetId));
            Assert.IsTrue(string.IsNullOrWhiteSpace(targetId));
        }
        
        [TestMethod]
        public void CanSeeExits()
        {
            var flatEarth = GetRoomInitialVisibilityTestData();

            var response = flatEarth.Do("exits");
            Assert.IsTrue(response.Success);
            Assert.IsTrue(string.Compare(response.Text, "EXITS ARE: NORTH.", StringComparison.CurrentCultureIgnoreCase) == 0);
            
            flatEarth.GetCurrentRoom().GetAnyExit("WEST")!.Discovered = true;
            response = flatEarth.Do("exits");
            Assert.IsTrue(response.Success);
            Assert.IsTrue(string.Compare(response.Text, "EXITS ARE: NORTH AND WEST.", StringComparison.CurrentCultureIgnoreCase) == 0);

            flatEarth.GetCurrentRoom().AddExit(new Exit("SOUTH", "5,4"));
            response = flatEarth.Do("exits");
            Assert.IsTrue(response.Success);
            Assert.IsTrue(string.Compare(response.Text, "EXITS ARE: NORTH, WEST AND SOUTH.", StringComparison.CurrentCultureIgnoreCase) == 0);

            flatEarth.GetCurrentRoom().AddExit(new Exit("EAST", "4,5"));
            response = flatEarth.Do("exits");
            Assert.IsTrue(response.Success);
            Assert.IsTrue(string.Compare(response.Text, "EXITS ARE: NORTH, WEST, SOUTH AND EAST.", StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void CanAdjustDescriptionResponseFromVisitCount()
        {
            var flatEarth = GetVisitDescriptionTestData();
            Assert.IsTrue(flatEarth.SetCurrentRoomId("1,1") == "1-1");
            Assert.IsTrue(flatEarth.Do("go south").Text == "2-1");
            Assert.IsTrue(flatEarth.Do("go north").Text == "1-2");
            Assert.IsTrue(flatEarth.Do("go south").Text == "2-2");
        }
        
        [TestMethod]
        public void CanAdjustLookResponseFromVisitCount()
        {
            var flatEarth = GetLookTestData();
            Assert.IsTrue(flatEarth.Do("look").Text == "1-1");
            Assert.IsTrue(flatEarth.Do("look").Text == "1-2");
            Assert.IsTrue(flatEarth.Do("look").Text == "1-2");
            flatEarth.Do("go south");
            Assert.IsTrue(flatEarth.Do("look").Text == "2-1");
            Assert.IsTrue(flatEarth.Do("look").Text == "2-2");
            Assert.IsTrue(flatEarth.Do("look").Text == "2-2");
            flatEarth.Do("go north");
            Assert.IsTrue(flatEarth.Do("look").Text == "1-1");
            Assert.IsTrue(flatEarth.Do("look").Text == "1-2");
            Assert.IsTrue(flatEarth.Do("look").Text == "1-2");
        }
    }
}
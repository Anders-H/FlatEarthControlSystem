using System;
using FlatEarthControlSystem;
using FlatEarthControlSystem.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class RoomBehaviourTests
    {

        [TestMethod]
        public void CanAdjustDescriptionResponseFromVisitCount()
        {
            var flatEarth = new FlatEarth();
            flatEarth.Load(@"
BEGIN ROOM 1,1

   BEGIN EXIT SOUTH: 1,2
   END EXIT

   FIRST DESCRIPTION: 1-1
   DESCRIPTION: 1-2

END ROOM

BEGIN ROOM 1,2

   BEGIN EXIT NORTH: 1,1
   END EXIT

   FIRST DESCRIPTION: 2-1
   DESCRIPTION: 2-2

END ROOM

CURRENT ROOM 1,1
");
            Assert.IsTrue(flatEarth.GetCurrentRoom().GetDescription() == "1-1");
            Assert.IsTrue(flatEarth.Do("go south").Text == "2-1");
            Assert.IsTrue(flatEarth.Do("go north").Text == "1-2");
            Assert.IsTrue(flatEarth.Do("go south").Text == "2-2");
        }

        [TestMethod]
        public void CanAdjustLookResponseFromVisitCount()
        {
            var flatEarth = new FlatEarth();
            flatEarth.Load(@"
BEGIN ROOM 1,1

   BEGIN EXIT SOUTH: 1,2
   END EXIT


   FIRST LOOK: 1-1
   LOOK: 1-2

END ROOM

BEGIN ROOM 1,2

   BEGIN EXIT NORTH: 1,1
   END EXIT

   FIRST LOOK: 2-1
   LOOK: 2-2

END ROOM

CURRENT ROOM 1,1
");
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

        [TestMethod]
        public void CanGiveRoomDescriptionEvenWhenFirstDescriptionIsNotGiven()
        {
            var flatEarth = new FlatEarth();
            flatEarth.Load(@"
BEGIN ROOM 1,1
BEGIN EXIT SOUTH: 1,2
END EXIT
DESCRIPTION: This is room 1,1
END ROOM

BEGIN ROOM 1,2
BEGIN EXIT NORTH: 1,1
END EXIT
DESCRIPTION: This is room 1,2
END ROOM

CURRENT ROOM 1,1
");
            Assert.IsTrue(flatEarth.GetCurrentRoom().Description == "This is room 1,1");
            Assert.IsTrue(string.IsNullOrEmpty(flatEarth.GetCurrentRoom().FirstEntryDescription));

            Assert.IsTrue(
                string.Compare(
                    "This is room 1,1",
                    flatEarth.GetCurrentRoom().GetDescription(),
                    StringComparison.CurrentCultureIgnoreCase
                ) == 0
            );

            flatEarth.Do("go south");

            Assert.IsTrue("This is room 1,2".Is(flatEarth.GetCurrentRoom().GetDescription()));
        }

        [TestMethod]
        public void CanGiveRoomDescriptionEvenWhenOnlyFirstDescriptionIsGiven()
        {
            var flatEarth = new FlatEarth();
            flatEarth.Load(@"
BEGIN ROOM 1,1
BEGIN EXIT SOUTH: 1,2
END EXIT
FIRST DESCRIPTION: This is room 1,1
END ROOM

BEGIN ROOM 1,2
BEGIN EXIT NORTH: 1,1
END EXIT
FIRST DESCRIPTION: This is room 1,2
END ROOM

CURRENT ROOM 1,1
");
            Assert.IsTrue(flatEarth.GetCurrentRoom().Description.IsEmpty());
            Assert.IsTrue(flatEarth.GetCurrentRoom().FirstEntryDescription == "This is room 1,1");

            Assert.IsTrue(
                string.Compare(
                    "This is room 1,1",
                    flatEarth.GetCurrentRoom().GetDescription(),
                    StringComparison.CurrentCultureIgnoreCase
                ) == 0
            );

            flatEarth.Do("go south");

            Assert.IsTrue(
                string.Compare(
                    "This is room 1,2",
                    flatEarth.GetCurrentRoom().GetDescription(),
                    StringComparison.CurrentCultureIgnoreCase
                ) == 0
            );
        }

    }
}
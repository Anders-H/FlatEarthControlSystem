using System;
using FlatEarthControlSystem;
using FlatEarthControlSystem.ControlCommandParser.Words;
using Xunit;

namespace FlatEarthControlSystemTests
{
    public class RoomBehaviourTests
    {

        [Fact]
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
            Assert.True(flatEarth.GetCurrentRoom().GetDescription() == "1-1");
            Assert.True(flatEarth.Go(new Noun("SOUTH")).Text == "2-1");
            Assert.True(flatEarth.Go(new Noun("NORTH")).Text == "1-2");
            Assert.True(flatEarth.Go(new Noun("SOUTH")).Text == "2-2");
        }

        [Fact]
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

        [Fact]
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
            Assert.True(flatEarth.GetCurrentRoom().Description == "This is room 1,1");
            Assert.True(string.IsNullOrEmpty(flatEarth.GetCurrentRoom().FirstEntryDescription));

            Assert.True(
                string.Compare(
                    "This is room 1,1",
                    flatEarth.GetCurrentRoom().GetDescription(),
                    StringComparison.CurrentCultureIgnoreCase
                ) == 0
            );

            flatEarth.Go(new Noun("SOUTH"));

            Assert.True(
                string.Compare(
                    "This is room 1,2",
                    flatEarth.GetCurrentRoom().GetDescription(),
                    StringComparison.CurrentCultureIgnoreCase
                ) == 0
            );
        }

        [Fact]
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
            Assert.True(string.IsNullOrEmpty(flatEarth.GetCurrentRoom().Description));
            Assert.True(flatEarth.GetCurrentRoom().FirstEntryDescription == "This is room 1,1");

            Assert.True(
                string.Compare(
                    "This is room 1,1",
                    flatEarth.GetCurrentRoom().GetDescription(),
                    StringComparison.CurrentCultureIgnoreCase
                ) == 0
            );

            flatEarth.Go(new Noun("SOUTH"));

            Assert.True(
                string.Compare(
                    "This is room 1,2",
                    flatEarth.GetCurrentRoom().GetDescription(),
                    StringComparison.CurrentCultureIgnoreCase
                ) == 0
            );
        }

    }
}
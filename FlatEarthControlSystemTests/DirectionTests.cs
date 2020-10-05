using FlatEarthControlSystem.ControlCommandParser.WordTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class DirectionTests
    {
        [DataRow("north", "NORTH")]
        [DataRow("n", "NORTH")]
        [DataRow("ne", "NORTH-EAST")]
        [DataRow("n-e", "NORTH-EAST")]
        [DataRow("north east", "NORTH-EAST")]
        [DataRow("north-east", "NORTH-EAST")]
        [DataRow("northeast", "NORTH-EAST")]
        [DataRow("east", "east")]
        [DataRow("e", "east")]
        [DataRow("se", "SOUTH-EAST")]
        [DataRow("s-e", "SOUTH-EAST")]
        [DataRow("south east", "SOUTH-EAST")]
        [DataRow("south-east", "SOUTH-EAST")]
        [DataRow("southeast", "SOUTH-EAST")]
        [DataRow("south", "south")]
        [DataRow("s", "south")]
        [DataRow("sw", "SOUTH-WEST")]
        [DataRow("s-w", "SOUTH-WEST")]
        [DataRow("south west", "SOUTH-WEST")]
        [DataRow("south-west", "SOUTH-WEST")]
        [DataRow("southwest", "SOUTH-WEST")]
        [DataRow("west", "WEST")]
        [DataRow("w", "WEST")]
        [DataRow("nw", "NORTH-WEST")]
        [DataRow("n-w", "NORTH-WEST")]
        [DataRow("north west", "NORTH-WEST")]
        [DataRow("north-west", "NORTH-WEST")]
        [DataRow("northwest", "NORTH-WEST")]
        [DataTestMethod]
        public void CanConvertNoundToDirection(string source, string target)
        {
            var noun = new Noun(source);
            Assert.IsNotNull(noun);

            var direction = Direction.FromNoun(noun);
            Assert.IsNotNull(direction);
            Assert.IsTrue(direction!.ToString() == target);
        }

        [TestMethod]
        public void NonDirectionNounsCannotConvertToDirections()
        {
            var noun = new Noun("FROG");
            var direction = Direction.FromNoun(noun);
            Assert.IsNull(direction);
        }
    }
}
using FlatEarthControlSystem.ControlCommandParser.WordTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class DirectionTests
    {
        [DataRow("north", "North")]
        [DataRow("n", "North")]
        [DataRow("ne", "North-East")]
        [DataRow("n-e", "North-East")]
        [DataRow("north east", "North-East")]
        [DataRow("north-east", "North-East")]
        [DataRow("northeast", "North-East")]
        [DataRow("east", "East")]
        [DataRow("e", "East")]
        [DataRow("se", "South-East")]
        [DataRow("s-e", "South-East")]
        [DataRow("south east", "South-East")]
        [DataRow("south-east", "South-East")]
        [DataRow("southeast", "South-East")]
        [DataRow("south", "South")]
        [DataRow("s", "South")]
        [DataRow("sw", "South-West")]
        [DataRow("s-w", "South-West")]
        [DataRow("south west", "South-West")]
        [DataRow("south-west", "South-West")]
        [DataRow("southwest", "South-West")]
        [DataRow("west", "West")]
        [DataRow("w", "West")]
        [DataRow("nw", "North-West")]
        [DataRow("n-w", "North-West")]
        [DataRow("north west", "North-West")]
        [DataRow("north-west", "North-West")]
        [DataRow("northwest", "North-West")]
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
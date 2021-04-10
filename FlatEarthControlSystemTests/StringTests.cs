using FlatEarthControlSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class StringTests
    {
        [DataRow("abc 123 ABC 456", "abc 123 ABC 456")]
        [DataRow("a!#b. ,c", "ab c")]
        [DataTestMethod]
        public void AllowBasicCharacters(string input, string output) =>
            Assert.IsTrue(output == input.OnlyBasicCharacters());

        [DataRow("abc", true)]
        [DataRow("ab c", false)]
        [DataTestMethod]
        public void CanIdentifySingleWord(string input, bool result) =>
            Assert.IsTrue(input.IsSingleWord() == result);
    }
}
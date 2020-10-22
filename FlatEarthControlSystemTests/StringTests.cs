using FlatEarthControlSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class StringTests
    {
        [DataRow(" jag  är glad   ", "jag är glad")]
        [DataRow("mixed  Case", "mixed Case")]
        [DataTestMethod]
        public void CanMiddletrimText(string input, string output) =>
            Assert.IsTrue(output == input.MiddleTrim());

        [DataRow("abc 123 ABC 456", "abc 123 ABC 456")]
        [DataRow("a!#b. ,c", "ab c")]
        [DataTestMethod]
        public void AllowBasicCharacters(string input, string output) =>
            Assert.IsTrue(output == input.OnlyBasicCharacters());
    }
}
using FlatEarthControlSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class StringTests
    {
        [DataRow(" jag  är glad   ", "JAG ÄR GLAD")]
        [DataTestMethod]
        public void CanMiddletrimText(string input, string output) =>
            Assert.IsTrue(output == input.MiddleTrim());
    }
}
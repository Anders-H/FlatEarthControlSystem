using FlatEarthControlSystem;
using Xunit;

namespace FlatEarthControlSystemTests
{
    public class StringTests
    {
        [Theory]
        [InlineData(" jag  är glad   ", "JAG ÄR GLAD")]
        public void CanMiddletrimText(string input, string output) =>
            Assert.True(output == input.MiddleTrim());
    }
}
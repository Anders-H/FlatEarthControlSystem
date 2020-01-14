using System;
using System.Linq;
using FlatEarthControlSystem.PostProcessing;
using FlatEarthControlSystem.PreProcessing;
using Xunit;

namespace FlatEarthControlSystemTests
{
    public class PrePostProcessorTests
    {
        [Fact]
        public void PrePostProcessorIntentionRepresentation()
        {
            var preValues = Enum.GetValues(typeof(PreProcessorIntention)).Cast<PreProcessorIntention>()
                .Where(x => x != PreProcessorIntention.ParseFailed)
                .ToList();
            
            var postValues = Enum.GetValues(typeof(PostProcessorIntention)).Cast<PostProcessorIntention>()
                .Where(x => x != PostProcessorIntention.Custom && x != PostProcessorIntention.None)
                .ToList();
            
            foreach (var preValue in preValues)
                Assert.Contains(postValues, x => x.ToString() == preValue.ToString());
            
            foreach (var postValue in postValues)
                Assert.Contains(postValues, x => x.ToString() == postValue.ToString());
        }

        [Fact]
        public void CanPassThroughBehaviour()
        {
            
        }
    }
}
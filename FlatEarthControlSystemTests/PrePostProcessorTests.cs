using System;
using System.Linq;
using FlatEarthControlSystem.PostProcessing;
using FlatEarthControlSystem.PreProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatEarthControlSystemTests
{
    [TestClass]
    public class PrePostProcessorTests
    {
        [TestMethod]
        public void PrePostProcessorIntentionRepresentation()
        {
            var preValues = Enum.GetValues(typeof(PreProcessorIntention)).Cast<PreProcessorIntention>()
                .Where(x => x != PreProcessorIntention.ParseFailed)
                .ToList();
            
            var postValues = Enum.GetValues(typeof(PostProcessorIntention)).Cast<PostProcessorIntention>()
                .Where(x => x != PostProcessorIntention.Custom && x != PostProcessorIntention.None)
                .ToList();
            
            foreach (var preValue in preValues)
                Assert.IsTrue(postValues.Exists(x => x.ToString() == preValue.ToString()));
            
            foreach (var postValue in postValues)
                Assert.IsTrue(postValues.Exists(x => x.ToString() == postValue.ToString()));
        }

        [TestMethod]
        public void CanPassThroughBehaviour()
        {
            
        }
    }
}
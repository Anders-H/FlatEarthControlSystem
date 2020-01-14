using FlatEarthControlSystem.ControlCommandParser;

namespace FlatEarthControlSystem.PreProcessing
{
    public class PreProcessorArguments
    {
        public string SourceText { get; set; }
        public bool Cancel { get; set; }
        public string CancelText { get; set; }
        public PreProcessorIntention PreProcessorIntention { get; internal set; }
        public SuggestedCommand SuggestedCommand { get; set; }
    }
}
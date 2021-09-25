namespace FlatEarthControlSystem.PreProcessing
{
    public class PreProcessorArguments
    {
        public string SourceText { get; set; }
        public bool Cancel { get; set; }
        public string CancelText { get; set; }
        public PreProcessorIntention PreProcessorIntention { get; internal set; }

        public PreProcessorArguments() : this("", false, "", PreProcessorIntention.Empty)
        {
        }

        public PreProcessorArguments(string sourceText, bool cancel, string cancelText, PreProcessorIntention preProcessorIntention)
        {
            SourceText = sourceText;
            Cancel = cancel;
            CancelText = cancelText;
            PreProcessorIntention = preProcessorIntention;
        }
    }
}
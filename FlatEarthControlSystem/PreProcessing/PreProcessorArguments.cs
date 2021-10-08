namespace FlatEarthControlSystem.PreProcessing
{
    public class PreProcessorArguments
    {
        public string SourceText { get; set; }
        public bool Cancel { get; set; }
        public string CancelText { get; set; }

        public PreProcessorArguments() : this("", false, "")
        {
        }

        public PreProcessorArguments(string sourceText, bool cancel, string cancelText)
        {
            SourceText = sourceText;
            Cancel = cancel;
            CancelText = cancelText;
        }
    }
}
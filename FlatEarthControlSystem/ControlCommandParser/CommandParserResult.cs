namespace FlatEarthControlSystem.ControlCommandParser
{
    public class CommandParserResult
    {
        public const string GeneralErrorMessage = "I DON'T UNDERSTAND.";
        public bool Success { get; }
        public string? Message { get; }
        public Command? Result { get; }

        private CommandParserResult(bool success, string? message, Command? result)
        {
            Success = success;
            Message = message;
            Result = result;
        }

        internal static CommandParserResult CreateSuccessResult(Command result) =>
            new CommandParserResult(true, null, result);
        
        internal static CommandParserResult CreateFailResult(string message) =>
            new CommandParserResult(false, message, null);
        
        internal static CommandParserResult CreateFailResult() =>
            new CommandParserResult(false, GeneralErrorMessage, null);
    }
}
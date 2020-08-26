using System;
using FlatEarthControlSystem.PreProcessing;

namespace FlatEarthControlSystem.ControlCommandParser
{
    public class CommandParserResult
    {
        public const string GeneralErrorMessage = "I DON'T UNDERSTAND.";
        public bool Success { get; }
        public string? Message { get; }
        public SuggestedCommand? Result { get; }
        public PreProcessorIntention? Intention { get; }

        public string? Verb { get; }
        public string? ActOnObject { get; }
        public string? UsingObject { get; }

        private CommandParserResult(bool success, string? message, SuggestedCommand? result, PreProcessorIntention? intention, string? verb, string? actOnObject, string? usingObject)
        {
            Success = success;
            Message = message;
            Result = result;
            Intention = intention;
            if (Success && !Intention.HasValue)
                throw new ArgumentException("Missing intention.");
            Verb = verb;
            ActOnObject = actOnObject;
            UsingObject = usingObject;
        }

        internal static CommandParserResult CreateSuccessResult(SuggestedCommand result, PreProcessorIntention intention, string? verb, string? actOnObject, string? usingObject) =>
            new CommandParserResult(true, null, result, intention, verb, actOnObject, usingObject);
        
        internal static CommandParserResult CreateFailResult(string message) =>
            new CommandParserResult(false, message, null, null, null, null, null);
        
        internal static CommandParserResult CreateFailResult() =>
            new CommandParserResult(false, GeneralErrorMessage, null, null, null, null, null);
    }
}
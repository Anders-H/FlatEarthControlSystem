using System;
using FlatEarthControlSystem.PreProcessing;

namespace FlatEarthControlSystem.ControlCommandParser
{
    public class CommandParserResult
    {
        
        public bool Success { get; }
        
        public string? Message { get; }
        
        public SuggestedCommand? Result { get; }
        
        public PreProcessorIntention? Intention { get; }

        private CommandParserResult(
            bool success,
            string? message,
            SuggestedCommand? result,
            PreProcessorIntention? intention
        )
        {
            Success = success;
            Message = message;
            Result = result;
            Intention = intention;
            if (Success && !Intention.HasValue)
                throw new ArgumentException("Missing intention.");
        }

        internal static CommandParserResult CreateSuccessResult(SuggestedCommand result, PreProcessorIntention intention) =>
            new CommandParserResult(true, null, result, intention);
        
        internal static CommandParserResult CreateFailResult(string message) =>
            new CommandParserResult(false, message, null, null);
        
        internal static CommandParserResult CreateFailResult() =>
            new CommandParserResult(false, Phrases.IdontUnderstand, null, null);
    }
}
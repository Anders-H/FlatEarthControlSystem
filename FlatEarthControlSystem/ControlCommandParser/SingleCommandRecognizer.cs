namespace FlatEarthControlSystem.ControlCommandParser
{
    public class SingleCommandRecognizer
    {
        private readonly string _commandString;

        public SingleCommandRecognizer(string commandString)
        {
            _commandString = commandString;
        }

        public bool IsLook()
        {
            return false; //TODO
        }

        public bool IsGoNorth()
        {
            return false; //TODO
        }
    }
}
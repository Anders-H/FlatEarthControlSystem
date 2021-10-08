using FlatEarthControlSystem.WorldDefinition;

namespace FlatEarthControlSystem.PostProcessing
{
    public class PostProcessorArguments
    {
        public World World { get; }
        public Room CurrentRoom { get; set; }
        public string ExtraText { get; set; }

        public PostProcessorArguments(World world, Room currentRoom)
        {
            World = world;
            CurrentRoom = currentRoom;
            ExtraText = "";
        }
    }
}
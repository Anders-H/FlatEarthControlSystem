using FlatEarthControlSystem;
using FlatEarthControlSystem.WorldDefinition;

namespace FlatEarthControlSystemTests.Common
{
    public class FlatEarchControlBase
    {
        protected FlatEarth GetCanNavigateTestData()
        {
            var flatEarth = new FlatEarth();
            
            var r1 = new Room("5,5");
            r1.CreateExit("NORTH", "5,4");
            flatEarth.World.AddRoom(r1);
            
            var r2 = new Room("5,4");
            r2.CreateExit("SOUTH", "5,5");
            flatEarth.World.AddRoom(r2);
            
            flatEarth.SetCurrentRoomId("5,5");
            return flatEarth;
        }

        protected FlatEarth GetRoomInitialVisibilityTestData()
        {
            var flatEarth = new FlatEarth();
            
            var r1 = new Room("5,5");
            r1.CreateExit("NORTH", "5,4");
            r1.CreateExit("WEST", "4,5").Discovered = false;
            flatEarth.World.AddRoom(r1);
            
            var r2 = new Room("5,4");
            r2.CreateExit("SOUTH", "5,5");
            flatEarth.World.AddRoom(r2);
            
            var r3 = new Room("4,5");
            r3.CreateExit("EAST", "5,5");
            flatEarth.World.AddRoom(r3);
            
            flatEarth.SetCurrentRoomId("5,5");
            return flatEarth;
        }
    }
}
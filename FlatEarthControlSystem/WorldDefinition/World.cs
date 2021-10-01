using System;
using System.Collections.Generic;
using System.Linq;

namespace FlatEarthControlSystem.WorldDefinition
{
    public class World
    {
        private List<Room> Rooms { get; }

        public World()
        {
            Rooms = new List<Room>();
        }

        public int RoomCount =>
            Rooms.Count;
        
        public bool RoomExist(string id) =>
            Rooms.Exists(x =>
                string.Compare(x.Id, id, StringComparison.CurrentCultureIgnoreCase) == 0);

        public void AddRoom(Room room)
        {
            room.Parent = this;
            Rooms.Add(room);
        }

        public Room? GetRoom(string id) =>
            Rooms.FirstOrDefault(x => x.Id == id);

        public Room? GetRoom(int index) =>
            index >= 0 && Rooms.Count > index
                ? Rooms[index]
                : null;

        public List<Room> GetAllRooms() =>
            Rooms;
    }
}
using System;
using System.Linq;
using FlatEarthControlSystem.WorldDefinition;

namespace FlatEarthControlSystem.WorldDefinitionParser
{
    public class WorldParser
    {
        private readonly string _sourceData;

        public WorldParser(string sourceData)
        {
            _sourceData = sourceData;
        }

        public World Parse(out string startRoomId)
        {
            startRoomId = "";
            
            var rows = _sourceData
                .Split(new [] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(
                    mobile => mobile.Trim()).Where(s => s != string.Empty
                )
                .ToList();

            const string uppercase = "UPPERCASE";

            const string currentRoom = "CURRENT ROOM ";
            
            const string beginRoom = "BEGIN ROOM ";
            const string endRoom = "END ROOM";
            
            const string beginExit = "BEGIN EXIT ";
            const string notDiscovered = "NOT DISCOVERED";
            const string endExit = "END EXIT";

            const string firstDescription = "FIRST DESCRIPTION:";
            const string description = "DESCRIPTION:";

            const string firstLook = "FIRST LOOK:";
            const string look = "LOOK:";

            var world = new World();
            Room? r = null;
            Exit? e = null;
            
            while (rows.Count > 0)
            {
                var row = rows[0].Trim();
                rows.RemoveAt(0);

                if (row == uppercase)
                {
                    world.Uppercase = true;
                    continue;
                }

                if (row.StartsWith(currentRoom))
                {
                    var roomId = row.Substring(currentRoom.Length).Trim();
                    if (string.IsNullOrWhiteSpace(roomId))
                        throw new Exception($"Missing room ID: {row}");
                    startRoomId = roomId;
                    continue;
                }
                
                if (row.StartsWith(beginRoom))
                {
                    var roomId = row.Substring(beginRoom.Length).Trim();
                    if (string.IsNullOrWhiteSpace(roomId))
                        throw new Exception($"Missing room ID: {row}");
                    if (world.RoomExist(roomId))
                        throw new Exception($"Duplicate room ID: {roomId}");
                    r = new Room(roomId);
                    continue;
                }
                
                if (row == endRoom)
                {
                    if (r == null)
                        throw new Exception($"Unexpected: {endRoom}");
                    world.AddRoom(r);
                    r = null;
                    continue;
                }

                if (row.StartsWith(beginExit))
                {
                    var parts = row.Substring(beginExit.Length).Trim().Split(':');
                    if (parts.Length != 2)
                        throw new Exception($"Wrong arguments: {row}");
                    var directionName = parts[0].Trim();
                    var targetRoomId = parts[1].Trim();
                    if (r == null)
                        throw new Exception($"No open room: {row}");
                    e = new Exit(directionName, targetRoomId);
                    continue;
                }

                if (row == endExit)
                {
                    if (r == null || e == null)
                        throw new Exception($"Unexpected: {endExit}");
                    r.AddExit(e);
                    e = null;
                    continue;
                }

                if (row == notDiscovered)
                {
                    if (r == null || e == null)
                        throw new Exception($"Unexpected: {notDiscovered}");
                    e.Discovered = false;
                    continue;
                }

                if (row.StartsWith(firstDescription))
                {
                    if (r == null)
                        throw new Exception($"Unexpected: {firstDescription}");
                    r.FirstEntryDescription = row.Substring(firstDescription.Length).Trim();
                    continue;
                }
                
                if (row.StartsWith(description))
                {
                    if (r == null)
                        throw new Exception($"Unexpected: {description}");
                    r.Description = row.Substring(description.Length).Trim();
                    continue;
                }
                
                if (row.StartsWith(firstLook))
                {
                    if (r == null)
                        throw new Exception($"Unexpected: {firstLook}");
                    r.FirstLook = row.Substring(firstLook.Length).Trim();
                    continue;
                }
                
                if (row.StartsWith(look))
                {
                    if (r == null)
                        throw new Exception($"Unexpected: {look}");
                    r.Look = row.Substring(look.Length).Trim();
                    continue;
                }
                
                throw new Exception($"Unknown: {row}");
            }

            foreach (var room in world.GetAllRooms())
            {
                
            }
            
            return world;
        }
    }
}
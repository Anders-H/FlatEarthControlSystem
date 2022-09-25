using System;

namespace FlatEarthControlSystem.WorldDefinition;

public class Player
{
    private string? _currentRoomId;
    public Inventory Inventory { get; }

    public Player(FlatEarth flatEarth)
    {
        Inventory = new Inventory(flatEarth);
    }

    public string GetCurrentRoomId() =>
        _currentRoomId ?? "";

    public Room SetCurrentRoomId(string roomId, World world)
    {
        var room = world.GetRoom(roomId);
        if (room == null)
            throw new SystemException($"Room not found: {roomId}");
            
        room.VisitCount++;
        _currentRoomId = room.Id;
        room.LookCount = 0;
        return room;
    }
}
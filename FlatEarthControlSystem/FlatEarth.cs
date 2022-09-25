using System;
using FlatEarthControlSystem.PreProcessor;
using FlatEarthControlSystem.WorldDefinition;
using FlatEarthControlSystem.WorldDefinitionParser;

namespace FlatEarthControlSystem;

public class FlatEarth
{
    public World World { get; private set; }
    public Player Player { get; private set; }
    public WorldObjectList WorldObjects { get; }
    public IPreProcessor PreProcessor { get; }

    public FlatEarth() : this(new DefaultPreProcessor())
    {
    }

    public FlatEarth(IPreProcessor preProcessor)
    {
        World = new World();
        WorldObjects = new WorldObjectList();
        Player = new Player(this);
        PreProcessor = preProcessor;
    }

    public void Load(string data)
    {
        var worldParser = new WorldParser(data);
        World = worldParser.Parse(out var startRoomId);
        Player = new Player(this);
        Player.SetCurrentRoomId(startRoomId, World);
    }

    public string SetCurrentRoomId(string currentRoomId)
    {
        var room = Player.SetCurrentRoomId(currentRoomId, World);
        return room.GetDescription();
    }

    public CommandResult Do(string command)
    {
        command = PreProcessor.Process(command);

        var currentRoom = GetCurrentRoom();

        var result = new CommandParser(
            World,
            currentRoom,
            Player.Inventory
        ).Parse(command);

        var executer = new CommandExecuter(
            Player,
            World,
            currentRoom
        );

        return executer.Apply(result.Sentence);
    }

    public Room GetCurrentRoom()
    {
        var currentRoom = World.GetRoom(Player.GetCurrentRoomId());
        if (currentRoom == null)
            throw new SystemException("Room does not exist.");
        return currentRoom;
    }

    private static CommandResult Fail(string text) =>
        new CommandResult(false, text);
}
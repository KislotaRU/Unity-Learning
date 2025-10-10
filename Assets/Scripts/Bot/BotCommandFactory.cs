using System.Collections.Generic;
using UnityEngine;

public class BotCommandFactory : MonoBehaviour
{
    [SerializeField] private MovingConfiguration _movingConfiguration;
    [SerializeField] private CollectingConfiguration _collectingConfiguration;

    public List<IBotCommand> CreateCommandCollect(Item item, Vector3 storagePosition, StatValue resourcesCapacity)
    {
        List<IBotCommand> commands = new List<IBotCommand>()
        {
            new RotateCommand(_movingConfiguration, item.transform.position),
            new MoveCommand(_movingConfiguration, item.transform.position),
            new CollectCommand(_collectingConfiguration, item),
            new RotateCommand(_movingConfiguration, storagePosition),
            new MoveCommand(_movingConfiguration, storagePosition),
            new GiveCommand(item, resourcesCapacity)
        };

        return commands;
    }

    public List<IBotCommand> CreateCommandWait(Vector3 position)
    {
        List<IBotCommand> commands = new List<IBotCommand>()
        {
            new RotateCommand(_movingConfiguration, position),
            new MoveCommand(_movingConfiguration, position)
        };

        return commands;
    }

    public List<IBotCommand> CreateCommandBuild(Builder builder, Vector3 position)
    {
        List<IBotCommand> commands = new List<IBotCommand>()
        {
            new RotateCommand(_movingConfiguration, position),
            new MoveCommand(_movingConfiguration, position),
            new BuildCommand(builder)
        };

        return commands;
    }
}
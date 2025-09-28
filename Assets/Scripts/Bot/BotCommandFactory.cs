using System.Collections.Generic;
using UnityEngine;

public class BotCommandFactory : MonoBehaviour
{
    [SerializeField] private MovingConfiguration _movingConfiguration;
    [SerializeField] private CollectingConfiguration _collectingConfiguration;

    public List<IBotCommand> CreateCommandCollect(Item item, Vector3 storagePosition, StatValue resourcesCapacity)
    {
        List<IBotCommand> commands = new List<IBotCommand>();

        commands.Add(new RotateCommand(_movingConfiguration, item.transform.position));
        commands.Add(new MoveCommand(_movingConfiguration, item.transform.position));
        commands.Add(new CollectCommand(_collectingConfiguration, item));
        commands.Add(new RotateCommand(_movingConfiguration, storagePosition));
        commands.Add(new MoveCommand(_movingConfiguration, storagePosition));
        commands.Add(new GiveCommand(item, resourcesCapacity));

        return commands;
    }

    public List<IBotCommand> CreateCommandWait(Vector3 position)
    {
        List<IBotCommand> commands = new List<IBotCommand>();

        commands.Add(new RotateCommand(_movingConfiguration, position));
        commands.Add(new MoveCommand(_movingConfiguration, position));

        return commands;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Transform _resourceStorage;
    [SerializeField] private SpawnerBot _spawnerBot;
    [SerializeField] private BotCommandFactory _botCommandFactory;

    [SerializeField] private StatValue _resourcesCapacity;
    [SerializeField] private List<Bot> _freeBots;

    private Queue<Item> _targets;

    public StatValue ResourcesCapacity => _resourcesCapacity;
    public Transform ResourceStorage => _resourceStorage;

    private void Awake()
    {
        _freeBots = new List<Bot>();
        _targets = new Queue<Item>();
    }

    private void Update()
    {
        if (_targets.Count == 0)
        {
            Scan();

            if (_freeBots.Count > 0)
                HandleWait();
        }
        else
        {
            HandleCollect();
        }
    }

    private void OnEnable()
    {
        _spawnerBot.Spawned += RegisterBot;
    }

    private void OnDisable()
    {
        _spawnerBot.Spawned -= RegisterBot;
    }

    private void Scan()
    {
        _targets = _scanner.GetTargets();
    }

    private void HandleCollect()
    {
        List<IBotCommand> commands;
        Item item;

        if (TryGetFreeUnit(out Bot bot))
        {
            _freeBots.Remove(bot);
            item = _targets.Dequeue();

            commands = _botCommandFactory.CreateCommandCollect(item, ResourceStorage.position, ResourcesCapacity);

            bot.MissionCompleted += HandleCompletedCommand;

            bot.Execute(commands);
        }
    }

    private void HandleWait()
    {
        List<IBotCommand> commands;

        if (TryGetFreeUnit(out Bot bot))
        {
            _freeBots.Remove(bot);

            commands = _botCommandFactory.CreateCommandWait(bot.SpawnPosition);

            bot.MissionCompleted += HandleCompletedCommand;

            bot.Execute(commands);
        }
    }

    private void HandleCompletedCommand(Bot bot)
    {
        bot.MissionCompleted -= HandleCompletedCommand;

        _freeBots.Add(bot);
    }

    private void RegisterBot(Bot bot)
    {
        if (_freeBots.Remove(bot))
            UnregisterUnit(bot);

        bot.Destroyed += UnregisterUnit;

        _freeBots.Add(bot);
    }

    private void UnregisterUnit(Bot bot)
    {
        bot.Destroyed -= UnregisterUnit;

        _freeBots.Remove(bot);
    }

    private bool TryGetFreeUnit(out Bot bot) =>
        bot = _freeBots.Count > 0 ? _freeBots[0] : null;
}
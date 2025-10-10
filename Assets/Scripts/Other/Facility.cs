using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Transform _resourceStorage;
    [SerializeField] private SpawnerBot _spawnerBot;
    [SerializeField] private BotCommandFactory _botCommandFactory;

    [SerializeField] private float _costBot;
    [SerializeField] private float _costFacility;
    [SerializeField] private float _costBuilding;

    [SerializeField] private StatValue _resourcesCapacity;

    private List<Bot> _bots;
    private List<Bot> _freeBots;
    private Queue<Item> _targets;

    private Builder _builder;

    public StatValue ResourcesCapacity => _resourcesCapacity;
    public bool IsConstructionMode { get; private set; }

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (IsConstructionMode)
            HandleBuild();

        if (_targets.Count > 0)
        {
            HandleCollect();
        }
        else
        {
            Scan();
            HandleWait();
        }
    }

    private void OnEnable()
    {
        _spawnerBot.Spawned += RegisterBot;
        _resourcesCapacity.Changed += HandleResources;
    }

    private void OnDisable()
    {
        _spawnerBot.Spawned -= RegisterBot;
        _resourcesCapacity.Changed -= HandleResources;
    }

    public void Initialize()
    {
        _bots = new List<Bot>();
        _freeBots = new List<Bot>();
        _targets = new Queue<Item>();
    }

    public void AddBot(Bot bot)
    {
        RegisterBot(bot);

        _spawnerBot.Add(bot);
    }

    public void RemoveBot(Bot bot)
    {
        UnregisterBot(bot);

        _spawnerBot.Remove(bot);
    }

    public bool CanBuild()
    {
        return _bots.Count > _costBuilding;
    }

    public void SetConstructionMode(bool flag, Builder builder)
    {
        IsConstructionMode = flag;
        _builder = builder;
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

            commands = _botCommandFactory.CreateCommandCollect(item, _resourceStorage.position, _resourcesCapacity);

            bot.MissionCompleted += HandleCompletedCommand;

            bot.Execute(commands);
        }
    }

    private void HandleWait()
    {
        List<IBotCommand> commands;

        if (TryGetFreeUnit(out Bot bot))
        {
            if (bot.transform.position == bot.SpawnPosition)
                return;

            _freeBots.Remove(bot);

            commands = _botCommandFactory.CreateCommandWait(bot.SpawnPosition);

            bot.MissionCompleted += HandleCompletedCommand;

            bot.Execute(commands);
        }
    }

    private void HandleBuild()
    {
        List<IBotCommand> commands;

        if (_resourcesCapacity.Current < _costFacility)
            return;

        if (_builder.IsPlaced == false)
            return;

        if (TryGetFreeUnit(out Bot bot))
        {
            IsConstructionMode = false;
            _resourcesCapacity.Decrease(_costFacility);

            _freeBots.Remove(bot);

            commands = _botCommandFactory.CreateCommandBuild(_builder, _builder.BuildPosition);

            bot.Execute(commands);
        }
    }

    private void HandleResources()
    {
        if (IsConstructionMode)
            return;

        if (_resourcesCapacity.Current >= _costBot)
        {
            if (_spawnerBot.Spawn() != null)
                _resourcesCapacity.Decrease(_costBot);
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
            UnregisterBot(bot);

        bot.Destroyed += UnregisterBot;

        bot.SetFacility(this);

        _freeBots.Add(bot);
        _bots.Add(bot);
    }

    private void UnregisterBot(Bot bot)
    {
        bot.Destroyed -= UnregisterBot;

        _freeBots.Remove(bot);
        _bots.Remove(bot);
    }

    private bool TryGetFreeUnit(out Bot bot) =>
        bot = _freeBots.Count > 0 ? _freeBots[0] : null;
}
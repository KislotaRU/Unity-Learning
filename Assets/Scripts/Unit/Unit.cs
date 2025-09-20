using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private MovingConfiguration _movingConfiguration;
    [SerializeField] private CollectingConfiguration _collectingConfiguration;
    [SerializeField] private Transform _hand;

    public event Action<Unit> CompletedCommands;

    private CommandHandler _commandHandler;

    public UnitStateMachine StateMachine { get; private set; }
    public Transform Hand => _hand;
    public bool IsFree => _commandHandler.IsProcess == false;

    private void Awake()
    {
        InitializeStateMachine();

        _commandHandler = new CommandHandler();
    }

    private void OnEnable()
    {
        _commandHandler.Completed += HandleCommandHandler;
    }

    private void OnDisable()
    {
        _commandHandler.Completed -= HandleCommandHandler;
    }

    private void Update()
    {
        StateMachine?.Update();
    }

    public void AddCommand(ICommand command) =>
        _commandHandler.Enqueue(command);

    public void ResetCommands() =>
        _commandHandler.Clear();

    private void InitializeStateMachine()
    {
        var states = new Dictionary<UnitStateType, IState>
        {
            { UnitStateType.Idle, new IdleState(this) },
            { UnitStateType.Moving, new MovingState(this, _movingConfiguration) },
            { UnitStateType.Collecting, new CollectingState(this, _collectingConfiguration) },
            { UnitStateType.Giving, new GivingState(this) }
        };

        StateMachine = new UnitStateMachine(states);
    }

    private void HandleCommandHandler() =>
        CompletedCommands?.Invoke(this);
}
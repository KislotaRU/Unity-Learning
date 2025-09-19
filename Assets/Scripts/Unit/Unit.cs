using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private MovingConfiguration _movingConfiguration;

    public event Action<Unit> CompletedCommand;

    private UnitStateMachine _stateMachine;
    private CommandHandler _commandHandler;

    public UnitStateMachine StateMachine => _stateMachine;
    public bool IsFree => _commandHandler.IsProcess == false;

    private void Awake()
    {
        InitializeStateMachine();

        _commandHandler = new CommandHandler();
    }

    private void OnEnable()
    {
        _commandHandler.CompletedCommand += HandleCommandHandler;
    }

    private void OnDisable()
    {
        _commandHandler.CompletedCommand -= HandleCommandHandler;
    }

    private void Update()
    {
        _stateMachine?.Update();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<UnitStateType, IState>
        {
            { UnitStateType.Idle, new IdleState(this) },
            { UnitStateType.Moving, new MovingState(this, _movingConfiguration) },
            { UnitStateType.Collecting, new CollectingState(this) }
        };

        _stateMachine = new UnitStateMachine(states);
    }

    public void AddCommand(ICommand command) =>
        _commandHandler.Enqueue(command);

    public void ResetCommands() =>
        _commandHandler.Clear();

    private void HandleCommandHandler() =>
        CompletedCommand?.Invoke(this);
}
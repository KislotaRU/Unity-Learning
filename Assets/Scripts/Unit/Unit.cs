using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private MovingStateConfiguration _movingConfiguration;

    public event Action<Unit, UnitStateType> ChangedState;

    private UnitStateMachine _stateMachine;
    private HandlerCommand _handlerCommand;

    public UnitStateMachine StateMachine => _stateMachine;
    public UnitStateType CurrentStateType => _stateMachine.CurrentStateType;
    public UnitStateType PreviousStateType => _stateMachine.PreviousStateType;

    private void Awake()
    {
        InitializeStateMachine();

        _handlerCommand = new HandlerCommand();
    }

    private void OnEnable()
    {
        _stateMachine.ChangedState += HandleChangedState;
    }

    private void OnDisable()
    {
        _stateMachine.ChangedState -= HandleChangedState;
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
        _handlerCommand.Enqueue(command);

    public void ResetCommands() =>
        _handlerCommand.Clear();

    private void HandleChangedState(UnitStateType newStateType) =>
        ChangedState?.Invoke(this, newStateType);
}
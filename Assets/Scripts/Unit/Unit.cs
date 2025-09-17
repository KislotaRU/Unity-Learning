using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private MovingStateConfiguration _movingConfiguration;

    public event Action<Unit, UnitStateType> ChangedState;

    private UnitStateMachine _stateMachine;

    public UnitStateType CurrentStateType => _stateMachine.CurrentStateType;
    public UnitStateType PreviousStateType => _stateMachine.PreviousStateType;

    private void Awake()
    {
        InitializeStateMachine();
        HandleIdle();
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

        _stateMachine.ChangedState += HandleChangedState;
    }

    private void HandleChangedState(UnitStateType newStateType)
    {
        ChangedState?.Invoke(this, newStateType);
    }

    public void HandleIdle()
    {
        _stateMachine.HandleState<IdleState>(UnitStateType.Idle, idleState => { });
    }

    public void HandleMoving(Vector3 targetPosition)
    {
        _stateMachine.HandleState<MovingState>(UnitStateType.Moving, movingState =>
        {
            movingState.SetTargetPosition(targetPosition);
        });
    }
}
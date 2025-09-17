using System;
using System.Collections.Generic;

public class UnitStateMachine
{
    private readonly Dictionary<UnitStateType, IState> _states;

    public event Action<UnitStateType> ChangedState;

    private IState _currentState;

    public UnitStateType CurrentStateType { get; private set; }
    public UnitStateType PreviousStateType { get; private set; }

    public void Update()
    {
        _currentState?.Update();
    }

    public UnitStateMachine(Dictionary<UnitStateType, IState> states)
    {
        _states = new Dictionary<UnitStateType, IState>(states);
    }

    public void HandleState<T>(UnitStateType newStateType, Action<T> setParameters) where T : class, IState
    {
        var newState = _states[newStateType] as T;

        setParameters?.Invoke(newState);

        SetState(newStateType, newState);
    }

    private void SetState(UnitStateType newStateType, IState newState)
    {
        _currentState?.Exit();

        PreviousStateType = CurrentStateType;

        CurrentStateType = newStateType;
        _currentState = newState;

        ChangedState?.Invoke(CurrentStateType);

        _currentState.Enter();
    }
}
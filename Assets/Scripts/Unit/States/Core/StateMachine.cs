using System;
using System.Collections.Generic;

public abstract class StateMachine<T> where T: Enum
{
    protected readonly Dictionary<T, IState> _states;

    public event Action<T> ChangedState;

    protected IState _currentState;

    public T CurrentStateType { get; protected set; }
    public T PreviousStateType { get; protected set; }

    public void Update() =>
        _currentState?.Update();

    public StateMachine(Dictionary<T, IState> states)
    {
        _states = new Dictionary<T, IState>(states);
    }

    public void HandleState<K>(T newStateType, Action<K> setParameters) where K : class, IState
    {
        var newState = _states[newStateType] as K;

        setParameters?.Invoke(newState);

        SetState(newStateType, newState);
    }

    private void SetState(T newStateType, IState newState)
    {
        _currentState?.Exit();

        PreviousStateType = CurrentStateType;

        CurrentStateType = newStateType;
        _currentState = newState;

        ChangedState?.Invoke(CurrentStateType);

        _currentState.Enter();
    }
}
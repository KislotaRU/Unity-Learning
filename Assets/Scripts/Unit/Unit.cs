using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    public event Action<Unit, UnitStateType> StateChanged;

    public Vector3 SpawnPosition { get; private set; }

    private Dictionary<UnitStateType, IUnitState> _states;
    private IUnitState _currentState;
    private UnitStateType _currentStateType;
    private UnitStateType _previousStateType;

    public float MoveSpeed => _moveSpeed;
    public UnitStateType CurrentStateType
    {
        get
        {
            return _currentStateType; 
        }

        private set
        {
            if (_currentStateType != value)
            {
                _currentStateType = value;

                StateChanged?.Invoke(this, value);
            }
        }
    }

    public UnitStateType PreviousStateType => _previousStateType;

    private void Awake()
    {
        InitializeStates();

        SetState(UnitStateType.Idle);
    }

    private void Update()
    {
        _currentState?.Update();
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        SpawnPosition = position;
    }

    public void HandleIdle()
    {
        SetState(UnitStateType.Idle);
    }

    public void HandleMoving(Vector3 position)
    {
        SetState(UnitStateType.Moving, position);
    }

    public void SetState(UnitStateType newStateType, params object[] args)
    {
        _currentState?.Exit();

        _previousStateType = CurrentStateType;
        CurrentStateType = newStateType;

        _currentState = _states[newStateType];

        if (_currentState is IParametrizedState parametrizedState)
            parametrizedState.SetParameters(args);

        _currentState.Enter();
    }

    private void InitializeStates()
    {
        _states = new Dictionary<UnitStateType, IUnitState>
        {
            { UnitStateType.Idle, new IdleState(this) },
            { UnitStateType.Moving, new MovingState(this) }
        };
    }
}
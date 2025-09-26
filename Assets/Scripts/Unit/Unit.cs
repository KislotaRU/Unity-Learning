using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private MoveCoroutine _moveCoroutine;
    [SerializeField] private RotationCoroutine _rotationCoroutine;
    [SerializeField] private CollectCoroutine _collectCoroutine;
    [SerializeField] private GiveCoroutine _giveCoroutine;

    [SerializeField] private Transform _hand;

    public event Action<Unit> Destroyed;

    public UnitStateMachine StateMachine { get; private set; }
    public Transform Hand => _hand;
    public SpawnZone SpawnZone { get; private set; }
    public Vector3 SpawnPosition { get; private set; }

    private Coroutine _currentSequence;

    public void Initialize(SpawnZone spawnZone, Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        transform.rotation = Quaternion.identity;

        SpawnZone = spawnZone;
        SpawnPosition = spawnPosition;
    }

    public void ExecuteSequence(IEnumerator sequence)
    {
        if (_currentSequence != null)
            StopCoroutine(_currentSequence);

        _currentSequence = StartCoroutine(RunSequence(sequence));
    }

    private IEnumerator RunSequence(IEnumerator sequence)
    {
        yield return sequence;

        _currentSequence = null;
    }

    public IEnumerator MoveTo(Vector3 position)
    {
        yield return _moveCoroutine.MoveTo(position);
    }

    public IEnumerator RotateTo(Vector3 position)
    {
        yield return _rotationCoroutine.RotateTo(position);
    }

    public IEnumerator Collect(Item item)
    {
        yield return _collectCoroutine.Collect(item, Hand);
    }

    public IEnumerator GiveItem(Item item)
    {
        yield return _giveCoroutine.GiveItem(item, Hand);
    }

    //private void InitializeStateMachine()
    //{
    //    var states = new Dictionary<UnitStateType, IState>
    //    {
    //        { UnitStateType.Idle, new IdleState(this) },
    //        { UnitStateType.Moving, new MovingState(this, _movingConfiguration) },
    //        { UnitStateType.Collecting, new CollectingState(this, _collectingConfiguration) },
    //        { UnitStateType.Giving, new GivingState(this) }
    //    };

    //    StateMachine = new UnitStateMachine(states);
    //}

    private void HandleDestroy() =>
        Destroyed?.Invoke(this);
}
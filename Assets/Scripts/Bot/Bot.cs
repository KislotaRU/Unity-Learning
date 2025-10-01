using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _storage;

    public event Action<Bot> MissionCompleted;
    public event Action<Bot> Destroyed;

    private Coroutine _currentSequence;

    public SpawnZone SpawnZone { get; private set; }
    public Vector3 SpawnPosition { get; private set; }
    public Facility CurrentFacility { get; private set; }
    public Transform Storage => _storage;

    public void Initialize(SpawnZone spawnZone, Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        transform.rotation = Quaternion.identity;

        SpawnZone = spawnZone;
        SpawnPosition = spawnPosition;
    }

    public void SetFacility(Facility facility)
    {
        CurrentFacility = facility;
    }

    public void Execute(List<IBotCommand> commands)
    {
        if (_currentSequence != null)
            StopCoroutine(_currentSequence);

        _currentSequence = StartCoroutine(ExecuteCommands(commands));
    }

    private IEnumerator ExecuteCommands(List<IBotCommand> commands)
    {
        foreach (IBotCommand command in commands)
        {
            yield return command.Execute(this);
        }

        MissionCompleted?.Invoke(this);

        _currentSequence = null;
    }

    private void HandleDestroy() =>
        Destroyed?.Invoke(this);
}
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneStorage : MonoBehaviour
{
    [SerializeField] private List<SpawnZone> _spawnZones;

    private List<SpawnZone> _lockSpawnZones;
    private List<SpawnZone> _unlockSpawnZones;

    public SpawnZone CurrentSpawnZone { get; private set; }
    public bool IsFreePosition => _unlockSpawnZones.Count > 0;

    private void Awake()
    {
        _spawnZones = new List<SpawnZone>();
        _lockSpawnZones = new List<SpawnZone>();

        RefreshChilds();

        if (_spawnZones.Count == 0)
        {
            enabled = false;
            return;
        }

        _unlockSpawnZones = new List<SpawnZone>(_spawnZones);
    }

    public Vector3 GetRandomPosition()
    {
        int indexSpawnZone;
        Vector3 randomPosition;

        RefreshSpawnZones();

        indexSpawnZone = Random.Range(0, _unlockSpawnZones.Count);
        CurrentSpawnZone = _unlockSpawnZones[indexSpawnZone];

        randomPosition = CurrentSpawnZone.GetRandomPosition();

        if (CurrentSpawnZone.IsFreeZone == false)
        {
            _unlockSpawnZones.Remove(CurrentSpawnZone);
            _lockSpawnZones.Add(CurrentSpawnZone);
        }

        return randomPosition;
    }

    public void RefreshSpawnZones()
    {
        _unlockSpawnZones = new List<SpawnZone>(_spawnZones);

        _lockSpawnZones.RemoveAll(spawnZone => spawnZone.IsFreeZone);

        foreach (SpawnZone lockSpawnZone in _lockSpawnZones)
            _unlockSpawnZones.Remove(lockSpawnZone);
    }

    [ContextMenu("Refresh Childs")]
    private void RefreshChilds()
    {
        _spawnZones.Clear();

        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).TryGetComponent(out SpawnZone spawnZone))
                _spawnZones.Add(spawnZone);
    }
}
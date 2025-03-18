using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private SpawnZone[] _spawnZones;

    private List<SpawnZone> _occupiedSpawnZones;
    private List<SpawnZone> _freeSpawnZones;
    private SpawnZone _currentSpawnZone;

    public SpawnZone CurrentSpawnZone => _currentSpawnZone;
    public bool IsFreeSpawnZone => _occupiedSpawnZones.Count < _spawnZones.Length;

    private void Awake()
    {
        if (_spawnZones == null)
            enabled = false;

        _occupiedSpawnZones = new List<SpawnZone>();
        _freeSpawnZones = new List<SpawnZone>();
    }

    public Vector2 GetRandomPosition()
    {
        int indexSpawnZone;
        Vector2 randomPosition;
        
        RefreshSpawnZones();

        indexSpawnZone = Random.Range(0, _freeSpawnZones.Count);
        _currentSpawnZone = _freeSpawnZones[indexSpawnZone];

        randomPosition = _currentSpawnZone.GetRandomPosition();

        if (_currentSpawnZone.IsFreePosition == false)
            _occupiedSpawnZones.Add(_currentSpawnZone);

        return randomPosition;
    }

    public void RefreshSpawnZones()
    {
        _freeSpawnZones = new List<SpawnZone>(_spawnZones);

        _occupiedSpawnZones.RemoveAll(spawnZone => spawnZone.IsFreePosition);

        foreach (SpawnZone occupiedSpawnZone in _occupiedSpawnZones)
            _freeSpawnZones.Remove(occupiedSpawnZone);
    }

    [ContextMenu("Refresh Child")]
    private void RefreshChild()
    {
        _spawnZones = new SpawnZone[transform.childCount];

        for (int i = 0; i < _spawnZones.Length; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out SpawnZone spawnZone))
                _spawnZones[i] = spawnZone;
        }
    }
}
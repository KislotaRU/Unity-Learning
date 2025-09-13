using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneStorage : MonoBehaviour
{
    [SerializeField] private List<SpawnZone> _spawnZones;

    private List<SpawnZone> _lockSpawnZones;
    private List<SpawnZone> _unlockSpawnZones;

    public SpawnZone CurrentSpawnZone { get; private set; }
    public bool IsFreeZone => _unlockSpawnZones.Count > 0;

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
        int index;
        Vector3 position;

        index = Random.Range(0, _unlockSpawnZones.Count);
        CurrentSpawnZone = _unlockSpawnZones[index];

        position = CurrentSpawnZone.GetRandomPosition();

        if (CurrentSpawnZone.IsFreePosition == false)
        {
            _unlockSpawnZones.Remove(CurrentSpawnZone);
            _lockSpawnZones.Add(CurrentSpawnZone);
        }

        return position;
    }

    public void ReleasePosition(SpawnZone spawnZone, Vector3 position)
    {
        spawnZone.ReleasePosition(position);

        if (spawnZone.IsFreePosition)
        {
            _lockSpawnZones.Remove(spawnZone);
            _unlockSpawnZones.Add(spawnZone);
        }
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
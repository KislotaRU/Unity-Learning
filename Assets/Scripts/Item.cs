using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> Collected;

    private Vector3 _spawnPosition;
    private SpawnZone _spawnZone;

    public void Initialize(SpawnZone spawnZone, Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        _spawnZone = spawnZone;
        _spawnPosition = position;
    }

    public void HandleCollect()
    {
        _spawnZone?.ReleasePosition(_spawnPosition);
        Collected?.Invoke(this);
    }
}
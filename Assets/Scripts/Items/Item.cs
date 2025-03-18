using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> ItemCollected;

    private Vector2 _spawnPosition;
    private SpawnZone _spawnZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
            Collect();
    }

    public void Initialize(SpawnZone spawnZone, Vector2 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        _spawnZone = spawnZone;
        _spawnPosition = position;
    }

    private void Collect()
    {
        _spawnZone?.ReleasePosition(_spawnPosition);
        ItemCollected?.Invoke(this);
    }
}
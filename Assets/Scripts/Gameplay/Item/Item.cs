using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Healer")]
    [SerializeField] private Healer _healer;

    public event Action<Item> ItemCollected;

    private Vector2 _spawnPosition;
    private SpawnZone _spawnZone;

    public void Initialize(SpawnZone spawnZone, Vector2 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        _spawnZone = spawnZone;
        _spawnPosition = position;
    }

    public void HandleCollect(Collector collector)
    {
        if (collector.TryGetComponent(out Health health))
            _healer.Heal(health);

        _spawnZone?.ReleasePosition(_spawnPosition);
        ItemCollected?.Invoke(this);
    }
}
using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Healer")]
    [SerializeField] private Healer _healer;

    public event Action<Item> ItemCollected;

    private Vector2 _spawnPosition;
    private SpawnZone _spawnZone;

    private void Awake()
    {
        _healer = GetComponent<Healer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _) && other.TryGetComponent(out Health health))
        {
            _healer.Heal(health);
            HandleCollect();
        }
    }

    public void Initialize(SpawnZone spawnZone, Vector2 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        _spawnZone = spawnZone;
        _spawnPosition = position;
    }

    private void HandleCollect()
    {
        _spawnZone?.ReleasePosition(_spawnPosition);
        ItemCollected?.Invoke(this);
    }
}
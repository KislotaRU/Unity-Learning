using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> Collected;

    public Vector3 SpawnPosition { get; private set; }
    public SpawnZone SpawnZone { get; private set; }

    public void Initialize(SpawnZone spawnZone, Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        SpawnZone = spawnZone;
        SpawnPosition = position;
    }

    public void HandleCollect()
    {
        Collected?.Invoke(this);
    }
}
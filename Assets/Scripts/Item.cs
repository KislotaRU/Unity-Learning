using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> ItemCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
            Collect();
    }

    public void Initialize(Vector2 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }

    private void Collect()
    {
        ItemCollected?.Invoke(this);
    }
}
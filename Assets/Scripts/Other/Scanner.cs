using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Scanner : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    private HashSet<Item> _scanningItems; 

    private BoxCollider _boxCollider;

    private void Awake()
    {
        _scanningItems = new HashSet<Item>();

        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider>();

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(_boxCollider.bounds.center, _boxCollider.size);
    }

    public Queue<Item> GetTargets()
    {
        Collider[] colliders = Physics.OverlapBox(_boxCollider.bounds.center, _boxCollider.size, Quaternion.identity, _targetLayer);
        Queue<Item> targets = new Queue<Item>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Item item) == false)
                continue;

            if (_scanningItems.Add(item) == false)
                continue;

            item.Destroyed += HandleDestroyItem;

            targets.Enqueue(item);
        }

        return targets;
    }

    private void HandleDestroyItem(Item item)
    {
        item.Destroyed -= HandleDestroyItem;

        _scanningItems.Remove(item);
    }
}
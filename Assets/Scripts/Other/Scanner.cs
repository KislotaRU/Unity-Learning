using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Scanner : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    private BoxCollider _boxCollider;

    private void Awake()
    {
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
        Collider[] targetsCollider = Physics.OverlapBox(_boxCollider.bounds.center, _boxCollider.size, Quaternion.identity, _targetLayer);
        Queue<Item> targets = new Queue<Item>();

        foreach (Collider target in targetsCollider)
        {
            if (target.TryGetComponent(out Item item))
                targets.Enqueue(item);
        }

        return targets;
    }
}
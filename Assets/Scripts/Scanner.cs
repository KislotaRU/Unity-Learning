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

    public List<Vector3> GetTargets()
    {
        Collider[] targetsCollider = Physics.OverlapBox(_boxCollider.bounds.center, _boxCollider.size, Quaternion.identity, _targetLayer);
        List<Vector3> targetsPosition = new List<Vector3>();

        foreach (Collider target in targetsCollider)
            targetsPosition.Add(target.transform.position);

        return targetsPosition;
    }
}
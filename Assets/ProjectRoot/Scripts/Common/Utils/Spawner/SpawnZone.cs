using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private BoxCollider _boxCollider;
    [Space]
    [SerializeField] private TypePosition _typePosition;

    private void Awake()
    {
        if (_boxCollider == null)
            throw new ArgumentNullException(nameof(_boxCollider));
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_boxCollider.bounds.center, _boxCollider.bounds.size);
    }

    public Vector3 GetPosition()
    {
        switch (_typePosition)
        {
            case TypePosition.Zero:
                return GetCenterPosition();

            case TypePosition.Random:
                return GetRandomPosition();

            default:
                break;
        }
        
        throw new ArgumentOutOfRangeException(nameof(TypePosition));
    }

    private Vector3 GetCenterPosition()
    {
        return _boxCollider.bounds.center;
    }

    private Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(_boxCollider.bounds.min.x, _boxCollider.bounds.max.x);
        float positionY = Random.Range(_boxCollider.bounds.min.y, _boxCollider.bounds.max.y);
        float positionZ = Random.Range(_boxCollider.bounds.min.z, _boxCollider.bounds.max.z);

        return new Vector3(positionX, positionY, positionZ);
    }
}
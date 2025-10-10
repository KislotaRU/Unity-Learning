using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnZone : MonoBehaviour
{
    private readonly float _offsetPosition = 0.5f;

    private List<Vector3> _positions;
    private List<Vector3> _lockPositions;
    private List<Vector3> _unlockPositions;

    private BoxCollider _boxCollider;

    public bool IsFreePosition => _unlockPositions.Count > 0;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();

        _positions = new List<Vector3>();
        _lockPositions = new List<Vector3>();

        GeneratePositions();

        if (_positions.Count == 0)
        {
            enabled = false;
            return;
        }

        _unlockPositions = new List<Vector3>(_positions);
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider>();

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_boxCollider.bounds.center, _boxCollider.bounds.size);
    }

    public Vector3 GetRandomPosition()
    {
        int index = Random.Range(0, _unlockPositions.Count);
        Vector3 position = _unlockPositions[index];

        _lockPositions.Add(position);
        _unlockPositions.Remove(position);

        return position;
    }

    public void ReleasePosition(Vector3 lockPosition)
    {
        if (_lockPositions.Remove(lockPosition))
            _unlockPositions.Add(lockPosition);
    }

    private void GeneratePositions()
    {
        Vector3Int dimensions = new Vector3Int((int)_boxCollider.bounds.size.x, (int)_boxCollider.bounds.size.y, (int)_boxCollider.bounds.size.z);
        Vector3 minPosition = _boxCollider.bounds.min + Vector3.one * _offsetPosition;
        int totalPositions = dimensions.x * dimensions.y * dimensions.z;

        for (int i = 0; i < totalPositions; i++)
        {
            int x = i % dimensions.x;
            int y = (i / dimensions.x) % dimensions.y;
            int z = i / (dimensions.x * dimensions.y);

            _positions.Add(new Vector3(
                minPosition.x + x,
                minPosition.y + y,
                minPosition.z + z
            ));
        }
    }
}
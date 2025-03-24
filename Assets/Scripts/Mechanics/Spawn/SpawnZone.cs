using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnZone : MonoBehaviour
{
    private readonly float _offsetPosition = 0.5f;

    private Vector2[] _positions;
    private List<Vector2> _occupiedPositions;
    private List<Vector2> _freePositions;

    private BoxCollider2D _boxCollider2D;
    private Bounds _bounds;

    public bool IsFreePosition => _occupiedPositions.Count < _positions.Length;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _bounds = _boxCollider2D.bounds;
        _occupiedPositions = new List<Vector2>();
        _freePositions = new List<Vector2>();

        FillArray();

        if (_positions.Length == 0)
            enabled = false;
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider2D == null)
            _boxCollider2D = GetComponent<BoxCollider2D>();

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size);
    }

    public Vector2 GetRandomPosition()
    {
        int indexPosition;
        _freePositions = new List<Vector2>(_positions);

        foreach (Vector2 occupiedPositions in _occupiedPositions)
            _freePositions.Remove(occupiedPositions);

        indexPosition = Random.Range(0, _freePositions.Count);

        _occupiedPositions.Add(_freePositions[indexPosition]);

        return _freePositions[indexPosition];
    }

    public void ReleasePosition(Vector2 position)
    {
        _occupiedPositions.Remove(position);
    }

    private void FillArray()
    {
        int positionCount = ((int)_bounds.size.x) * ((int)_bounds.size.y);

        float minPositionX = _bounds.min.x + _offsetPosition;
        float minPositionY = _bounds.min.y + _offsetPosition;

        _positions = new Vector2[positionCount];

        for (int x = 0; x < (int)_bounds.size.x; x++)
            for (int y = 0; y < (int)_bounds.size.y; y++)
                _positions[x] = new Vector2(minPositionX + x, minPositionY + y);
    }
}
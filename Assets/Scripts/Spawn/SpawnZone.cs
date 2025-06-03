using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnZone : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
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
        float positionX = transform.position.x;
        float positionY = Random.Range(_boxCollider2D.bounds.min.y, _boxCollider2D.bounds.max.y);

        return new Vector2(positionX, positionY);
    }
}
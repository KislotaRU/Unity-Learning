using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnZone : MonoBehaviour
{
    [SerializeField] private float _offsetPosition = 0.5f;

    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public Vector2 GetRandomPosition()
    {
        Bounds bounds = _boxCollider2D.bounds;

        int randomX = Random.Range((int)bounds.min.x, (int)bounds.max.x);
        int randomY = Random.Range((int)bounds.min.y, (int)bounds.max.y);

        Debug.Log($"bounds.min.x {bounds.min.x}");
        Debug.Log($"bounds.max.x {bounds.max.x}");
        Debug.Log($"bounds.min.y {bounds.min.y}");
        Debug.Log($"bounds.max.y {bounds.max.y}");
        Debug.Log($"bounds.min {bounds.min}");
        Debug.Log($"bounds.max {bounds.max}");
        Debug.Log($"bounds.size {bounds.size}");

        return new Vector2(randomX, randomY);
    }
}
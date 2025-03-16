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

        int minRandomX = (int)(bounds.min.x + _offsetPosition);
        int maxRandomX = (int)(bounds.max.x - _offsetPosition);
        int minRandomY = (int)(bounds.min.y + _offsetPosition);
        int maxRandomY = (int)(bounds.max.y - _offsetPosition);

        int randomX = Random.Range(minRandomX, maxRandomX);
        int randomY = Random.Range(minRandomY, maxRandomY);

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
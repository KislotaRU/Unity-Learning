using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Repulsiver : MonoBehaviour
{
    [SerializeField] private float _forceRepulsive;
    [SerializeField] private Vector2 _throwDirection;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 direction)
    {
        _rigidbody2D.AddForce(new Vector2(-direction.x * _throwDirection.x, _throwDirection.y) * _forceRepulsive, ForceMode2D.Force);
    }
}
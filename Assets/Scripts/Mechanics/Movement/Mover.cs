using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 4f;

    private Rigidbody2D _rigidbody2D;

    public float Speed { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        Speed = direction.x * _moveSpeed;

        _rigidbody2D.velocity = new Vector2(Speed, _rigidbody2D.velocity.y);
    }

    public void MoveToPosition(Vector2 targetPosition)
    {
        Speed = _moveSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed);
    }

    public void SetMoveSpeed(float moveSpeed) =>
        _moveSpeed = moveSpeed;
}
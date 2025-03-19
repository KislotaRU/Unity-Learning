using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 4f;

    public void Move(Rigidbody2D rigidbody2D, Vector2 direction)
    {
        rigidbody2D.velocity = new Vector2(direction.x * _moveSpeed, rigidbody2D.velocity.y);
    }
}
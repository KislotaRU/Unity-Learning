using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 0.5f;
    [SerializeField] private float _checkRadius = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody2D;

    private bool _isGrounded = false;
    private Vector2 position;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump(Collider2D collider2D, bool isJumping)
    {
        CheckGround(collider2D);

        if (_isGrounded && isJumping)
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround(Collider2D collider2D)
    {
        position = new Vector2(collider2D.bounds.center.x, collider2D.bounds.min.y);

        _isGrounded = Physics2D.OverlapCircle(position, _checkRadius, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(position, _checkRadius);
    }
}
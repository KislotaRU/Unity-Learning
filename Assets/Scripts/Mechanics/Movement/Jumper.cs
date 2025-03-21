using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _offsetBox = 0f;
    [SerializeField] private Vector2 _sizeBox;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();

        if (_sizeBox == Vector2.zero)
            _sizeBox = new Vector2(_collider2D.bounds.size.x, 1);
    }

    public void Jump(bool isJumping)
    {
        CheckGround();

        if (IsGrounded && isJumping)
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Vector2 position = new Vector2(_collider2D.bounds.center.x, _collider2D.bounds.min.y + _offsetBox);

        IsGrounded = Physics2D.OverlapBox(position, _sizeBox, 0.1f, _groundLayer);
    }
}
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Detector2D _jumpZone;

    private Rigidbody2D _rigidbody2D;

    public bool IsGrounded => _jumpZone.TryGetTarget(out Collider2D _);

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump(bool isJumping)
    {
        if (isJumping)
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
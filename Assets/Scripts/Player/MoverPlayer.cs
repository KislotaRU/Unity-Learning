using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class MoverPlayer : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    private const string Running = nameof(Running);
    private const string Jumping = nameof(Jumping);

    [SerializeField] Animator _animator;

    [SerializeField] float _moveSpeed = 4f;
    [SerializeField] float _jumpForce = 7f;

    [SerializeField] private float _offsetLandingY = -0.5f;
    [SerializeField] private float _offsetRadius = 0.2f;

    private bool _isFacingRight = true;
    private float _direction;
    private Vector3 _localScale;
    private bool _isGrounded = true;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        _direction = Input.GetAxis(Horizontal);

        if (_direction > 0 && _isFacingRight == false)
            FlipHorizontally();
        else if (_direction < 0 && _isFacingRight)
            FlipHorizontally();

        _rigidbody.velocity = new Vector2(_direction * _moveSpeed, _rigidbody.velocity.y);
        _animator.SetFloat(Running, Mathf.Abs(_direction));
    }

    private void Jump()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);

        if (_isGrounded)
            _animator.SetBool(Jumping, false);
        else
            _animator.SetBool(Jumping, true);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x ,transform.position.y + _offsetLandingY), _offsetRadius);

        _isGrounded = colliders.Length > 1;
    }

    private void FlipHorizontally()
    {
        _isFacingRight = !_isFacingRight;
        _localScale = transform.localScale;

        _localScale.x *= -1;
        transform.localScale = _localScale;
    }
}
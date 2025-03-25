using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Walker : MonoBehaviour
{
    [SerializeField] private float _offsetBox;
    [SerializeField] private Vector2 _sizeBox;
    [SerializeField] private LayerMask _groundLayer;

    private Collider2D _collider2D;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnDrawGizmos()
    {
        if (_collider2D == null)
            _collider2D = GetComponent<Collider2D>();

        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(new Vector2(_collider2D.bounds.center.x, _collider2D.bounds.min.y + _offsetBox), _sizeBox);
    }

    public bool CanWalk()
    {
        IsGrounded = Physics2D.OverlapBox(new Vector2(_collider2D.bounds.center.x, _collider2D.bounds.min.y + _offsetBox), _sizeBox, 0f, _groundLayer);

        return IsGrounded;
    }
}
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Viewer : MonoBehaviour
{
    [SerializeField, Range(1, 50)] private int _range;
    [SerializeField] private float _colliderDistance;
    [SerializeField] private float _timeFollowing;
    [SerializeField] private LayerMask _targetLayer;

    private Collider2D _collider2D;
    
    public Vector2 TargetPosition { get; private set; }
    public bool IsFollowing { get; private set; }

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnDrawGizmos()
    {
        if (_collider2D == null)
            _collider2D = GetComponent<Collider2D>();

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(_collider2D.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
                                                         new Vector3(_collider2D.bounds.size.x * _range, _collider2D.bounds.size.y, _collider2D.bounds.size.z));
    }

    public bool TryGetTarget(out Vector2 targetPosition)
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_collider2D.bounds.center + transform.right * _range * transform.localScale.x,
                                                      new Vector3(_collider2D.bounds.size.x, _collider2D.bounds.size.y, _collider2D.bounds.size.z),
                                                      0f, Vector2.right, 0f, _targetLayer);

        if (raycastHit2D.collider != null)
        {
            targetPosition = raycastHit2D.collider.transform.position;
            return true;
        }

        targetPosition = Vector2.zero;

        return false;
    }
}
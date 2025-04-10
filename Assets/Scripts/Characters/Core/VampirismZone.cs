using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class VampirismZone : MonoBehaviour
{
    //[Header("Parameters for detection")]
    //[SerializeField, Range(0, 20)] private int _range;
    //[SerializeField] private float _colliderDistance;
    //[SerializeField] private LayerMask _targetLayer;

    //private Collider2D _collider2D;

    //public bool IsTracking;

    //public bool IsExistsTargetPosition { get; private set; }

    //private void Awake()
    //{
    //    _collider2D = GetComponent<Collider2D>();
    //}

    //private void OnDrawGizmos()
    //{
    //    if (IsTracking == false)
    //        return;

    //    if (_collider2D == null)
    //        _collider2D = GetComponent<Collider2D>();

    //    Gizmos.color = Color.yellow;

    //    Gizmos.DrawWireCube(_collider2D.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
    //                        new Vector3(_collider2D.bounds.size.x * _range, _collider2D.bounds.size.y, _collider2D.bounds.size.z));
    //}

    //public bool TryGetTarget(out Vector2 targetPosition)
    //{
    //    Collider2D colliderHit2D = Physics2D.OverlapCircle(_collider2D.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
    //                                                    new Vector2(_collider2D.bounds.size.x * _range, _collider2D.bounds.size.y), 0f, _targetLayer);

    //    if (colliderHit2D != null)
    //    {
    //        targetPosition = colliderHit2D.transform.position;
    //        IsExistsTargetPosition = true;

    //        return true;
    //    }

    //    targetPosition = Vector2.zero;
    //    IsExistsTargetPosition = false;

    //    return false;
    //}
}
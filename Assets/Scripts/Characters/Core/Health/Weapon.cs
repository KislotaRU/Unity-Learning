using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Weapon : MonoBehaviour
{
    [Header("Parameters for detection")]
    [SerializeField, Range(0f, 4f)] private float _attackRange;
    [SerializeField] private float _colliderDistance;
    [SerializeField] private LayerMask _targetLayer;

    [Header("Parameter Weapon")]
    [SerializeField] private float _attackCooldown;

    private Collider2D _collider2D;

    private bool _isRecharged = true;

    public bool IsPunchAvailable { get; private set; }

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnDrawGizmos()
    {
        if (_collider2D == null)
            _collider2D = GetComponent<Collider2D>();

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(_collider2D.bounds.center + transform.right * _attackRange * transform.localScale.x * _colliderDistance,
                            new Vector3(_collider2D.bounds.size.x * _attackRange, _collider2D.bounds.size.y, _collider2D.bounds.size.z));
    }

    private IEnumerator RechargingAttack()
    {
        yield return new WaitForSeconds(_attackCooldown);

        _isRecharged = true;
    }

    public bool TryAttack(out Health targetHealth)
    {
        targetHealth = null;

        if (_isRecharged == false)
            return false;

        _isRecharged = false;

        StartCoroutine(RechargingAttack());

        Collider2D[] raycastHits2D = Physics2D.OverlapBoxAll(_collider2D.bounds.center + transform.right * _attackRange * transform.localScale.x * _colliderDistance,
                                                             new Vector2(_collider2D.bounds.size.x, _collider2D.bounds.size.y), 0f, _targetLayer);

        foreach (Collider2D raycastHit2D in raycastHits2D)
        {
            if (raycastHit2D.TryGetComponent(out Health health) == false)
                continue;

            if (health.gameObject == gameObject)
                continue;

            targetHealth = health;
            IsPunchAvailable = true;

            return true;
        }

        IsPunchAvailable = false;

        return false;
    }
}
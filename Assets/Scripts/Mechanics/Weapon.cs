using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Weapon : MonoBehaviour
{
    [SerializeField, Range(0f, 4f)] private float _range;
    [SerializeField] private float _colliderDistance;
    [SerializeField] private float _delayAttack;
    [SerializeField] private LayerMask _targetLayerMask;

    private Collider2D _collider2D;

    private bool _isRecharged = true;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnDrawGizmos()
    {
        if (_collider2D == null)
            _collider2D = GetComponent<Collider2D>();

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(_collider2D.bounds.center + transform.right * _range * transform.localScale.x * _colliderDistance,
                            new Vector3(_collider2D.bounds.size.x * _range, _collider2D.bounds.size.y, _collider2D.bounds.size.z));
    }

    private IEnumerator RechargingAttack()
    {
        yield return new WaitForSeconds(_delayAttack);

        _isRecharged = true;
    }

    public bool TryAttack(out Health targetHealth)
    {
        targetHealth = null;

        if (_isRecharged == false)
            return false;

        _isRecharged = false;

        StartCoroutine(RechargingAttack());

        RaycastHit2D[] raycastHits2D = Physics2D.BoxCastAll(_collider2D.bounds.center + transform.right * _range * transform.localScale.x,
                                                            new Vector2(_collider2D.bounds.size.x, _collider2D.bounds.size.y),
                                                            0f, Vector2.left, _targetLayerMask);


        foreach (RaycastHit2D raycastHit2D in raycastHits2D)
        {
            Debug.Log($"Произошла атака по {raycastHit2D.collider.name}");

            if (raycastHit2D.collider.TryGetComponent(out Health health))
            {
                targetHealth = health;

                return true;
            }
        }

        return false;
    }
}
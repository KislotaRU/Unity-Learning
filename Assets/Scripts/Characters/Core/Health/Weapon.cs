using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Weapon : MonoBehaviour
{
    [Header("Detector")]
    [SerializeField] private Detector2D _attackZone;

    [Header("Parameter Weapon")]
    [SerializeField] private float _attackCooldown;

    private bool _isRecharged = true;

    public bool IsPunchAvailable { get; private set; }

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

        if (_attackZone.TryGetTargets(out Collider2D[] targets))
        {
            foreach (Collider2D target in targets)
            {
                if (target.TryGetComponent(out Health health) == false)
                    continue;

                if (health.gameObject == gameObject)
                    continue;

                targetHealth = health;
                IsPunchAvailable = true;

                return true;
            }
        }

        IsPunchAvailable = false;

        return false;
    }
}
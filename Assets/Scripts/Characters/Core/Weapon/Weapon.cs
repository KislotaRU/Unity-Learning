using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Detector")]
    [SerializeField] protected Detector2D _attackZone;

    [Header("Parameter Weapon")]
    [SerializeField] protected Cooldown _cooldown;
    [SerializeField] protected bool _isAutoReload;
    [SerializeField] protected Damager _damager;

    public bool IsAttacking { get; protected set; }

    private void OnEnable()
    {
        _cooldown.Unloaded += HandleUnloaded;
    }

    private void OnDisable()
    {
        _cooldown.Unloaded -= HandleUnloaded;
    }

    public bool CanAttack()
    {
        return TryGetTarget(out Health targetHealth);
    }

    public virtual void Attack()
    {
        if (TryUnload() == false)
            return;

        IsAttacking = true;

        if (TryGetTarget(out Health targetHealth))
            _damager.Attack(targetHealth);
    }

    protected bool TryGetTarget(out Health targetHealth)
    {
        if (_attackZone.TryGetTargets(out Collider2D[] targets))
        {
            foreach (Collider2D target in targets)
            {
                if (target.TryGetComponent(out Health health) == false)
                    continue;

                if (health.gameObject == gameObject)
                    continue;

                targetHealth = health;

                return true;
            }
        }

        targetHealth = null;

        return false;
    }

    protected bool TryUnload()
    {
        if (_cooldown.IsFull == false)
            return false;

        _cooldown.Unload();

        return true;
    }
    protected void Reload()
    {
        if (_isAutoReload)
            _cooldown.Reload();
    }

    private void HandleUnloaded()
    {
        IsAttacking = false;

        Reload();
    }
}
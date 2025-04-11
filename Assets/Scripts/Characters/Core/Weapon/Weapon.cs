using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Detector")]
    [SerializeField] protected Detector2D _attackZone;

    [Header("Parameter Weapon")]
    [SerializeField] protected Cooldown _cooldown;
    [SerializeField] protected bool _isAutoRecharge;

    public bool IsPunchAvailable { get; private set; }

    private void OnEnable()
    {
        _cooldown.Devastated += Recharge;
    }

    private void OnDisable()
    {
        _cooldown.Devastated -= Recharge;
    }

    public void Recharge()
    {
        if (_isAutoRecharge)
            _cooldown.Replenish();
    }

    public void Discharge()
    {
        _cooldown.Replenish();
    }

    public virtual bool TryAttack(out Health targetHealth)
    {
        targetHealth = null;

        if (_cooldown.IsFull == false)
            return false;

        _cooldown.Diminish();

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
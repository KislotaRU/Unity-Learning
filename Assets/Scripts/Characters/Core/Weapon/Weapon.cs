using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Detector")]
    [SerializeField] protected Detector2D _attackZone;

    [Header("Parameter Weapon")]
    [SerializeField] protected Cooldown _cooldown;
    [SerializeField] protected bool _isAutoReload;

    public event Action Attacking;

    public bool IsPunchAvailable { get; private set; }
    public bool IsAutoReload => _isAutoReload;
    public bool IsAttacking { get; private set; }

    private void OnEnable()
    {
        _cooldown.Unloaded += Reload;
    }

    private void OnDisable()
    {
        _cooldown.Unloaded -= Reload;
    }

    public virtual bool TryAttack(out Health targetHealth)
    {
        targetHealth = null;

        if (_cooldown.IsFull == false)
            return false;
        
        _cooldown.Unload();

        Attacking?.Invoke();

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

    public bool TryReload()
    {
        if (_cooldown.IsEmpty)
        {
            _cooldown.Reload();

            return true;
        }

        return false;
    }

    public void Reload()
    {
        if (_isAutoReload)
            _cooldown.Reload();
    }
}
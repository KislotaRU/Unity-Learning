using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [Header("Detector")]
    [SerializeField] protected Detector2D _activeZone;

    [Header("Parameter Ability")]
    [SerializeField] protected Cooldown _cooldown;
    [SerializeField] protected bool _isAutoReload;

    public bool IsActive { get; protected set; }

    private void OnEnable()
    {
        _cooldown.Unloaded += HandleUnloaded;
    }

    private void OnDisable()
    {
        _cooldown.Unloaded -= HandleUnloaded;
    }

    public virtual void Activate()
    {
        if (TryUnload() == false)
            return;

        IsActive = true;
    }

    protected virtual bool TryGetTarget(out Health targetHealth)
    {
        if (_activeZone.TryGetTargets(out Collider2D[] targets))
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

    protected void HandleUnloaded()
    {
        IsActive = false;

        Reload();
    }
}
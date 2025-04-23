using System.Collections;
using UnityEngine;

public class Vampirism : Ability
{
    [Header("Animation")]
    [SerializeField] private VampirismAnimator _vampirismAnimator;

    [SerializeField] private Damager _damager;
    [SerializeField] private Healer _healer;
    [SerializeField] private Health _ownerHealth;

    private void FixedUpdate()
    {
        HandleAnimation();
    }

    public override void Activate()
    {
        base.Activate();

        StartCoroutine(PullingHealth());
    }

    protected override bool TryGetTarget(out Health targetHealth)
    {
        float distance;
        float minDistance = float.MaxValue;

        targetHealth = null;

        if (_activeZone.TryGetTargets(out Collider2D[] targets))
        {
            foreach (Collider2D target in targets)
            {
                if (target.TryGetComponent(out Health health) == false)
                    continue;

                if (health.gameObject == gameObject)
                    continue;

                distance = (transform.position - target.transform.position).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    targetHealth = health;
                }
            }
        }    

        return targetHealth != null;
    }

    private IEnumerator PullingHealth()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);

        while (IsActive)
        {
            if (TryGetTarget(out Health targetHealth))
            {
                _damager.Attack(targetHealth);

                _healer.SetValue(_damager.LastDamage);
                _healer.Heal(_ownerHealth);
            }

            yield return delay;
        }
    }

    private void HandleAnimation()
    {
        _vampirismAnimator.Setup(IsActive);
    }
}
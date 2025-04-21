using System.Collections;
using UnityEngine;

public class Vampirism : Weapon
{
    [Header("Animation")]
    [SerializeField] protected VampirismAnimator _vampirismAnimator;

    [SerializeField] private Healer _healer;
    [SerializeField] private Health _ownerHealth;

    private void Update()
    {
        HandleAnimation();
    }

    private IEnumerator PullingHealth()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);

        while (IsAttacking)
        {
            if (TryGetTarget(out Health targetHealth))
            {
                _damager.Attack(targetHealth);
                _healer.Heal(_ownerHealth);
            }

            yield return delay;
        }
    }

    public override void Attack()
    {
        if (TryUnload() == false)
            return;

        IsAttacking = true;

        StartCoroutine(PullingHealth());
    }

    private void HandleAnimation()
    {
        _vampirismAnimator.Setup(IsAttacking);
    }
}
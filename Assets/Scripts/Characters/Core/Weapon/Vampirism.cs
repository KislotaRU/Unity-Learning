using UnityEngine;

public class Vampirism : Weapon
{
    [SerializeField] private Healer _healer;
    [SerializeField] private Health _owner;

    //public override bool TryAttack(out Health targetHealth)
    //{
    //    targetHealth = null;

    //    if (_cooldown.IsFull == false)
    //        return false;

    //    _cooldown.Diminish();

    //    if (_attackZone.TryGetTargets(out Collider2D[] targets))
    //    {
    //        foreach (Collider2D target in targets)
    //        {
    //            if (target.TryGetComponent(out Health health) == false)
    //                continue;

    //            if (health.gameObject == gameObject)
    //                continue;

    //            targetHealth = health;

    //            return true;
    //        }
    //    }

    //    return false;
    //}
}
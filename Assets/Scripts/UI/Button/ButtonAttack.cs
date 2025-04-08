using UnityEngine;

[RequireComponent(typeof(Damager))]
public class ButtonAttack : ButtonAction
{
    [SerializeField] private Damager _damager;

    protected override void HandleClick()
    {
        _damager.Attack(_target);
    }
}
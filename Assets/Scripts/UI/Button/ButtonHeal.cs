using UnityEngine;

[RequireComponent(typeof(Healer))]
public class ButtonHeal : ButtonAction
{
    [SerializeField] private Healer _healer;

    protected override void HandleClick()
    {
        _healer.Heal(_target);
    }
}
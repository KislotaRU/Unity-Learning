using UnityEngine;

public class Katana : Weapon
{
    [Header("Animation")]
    [SerializeField] protected KatanaAnimator _katanaAnimator;

    private void Update()
    {
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        _katanaAnimator.Setup(IsAttacking);
    }
}
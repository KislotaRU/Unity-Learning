using UnityEngine;

public class Katana : Weapon
{
    [Header("Animation")]
    [SerializeField] protected KatanaAnimator _katanaAnimator;

    private void OnEnable()
    {
        Attacking += HandleAnimator;
    }

    private void OnDisable()
    {
        Attacking -= HandleAnimator;
    }

    private void HandleAnimator()
    {
        _katanaAnimator.Play();
    }
}
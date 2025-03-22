using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _baseDamage = 10;

    private Health _lastTargetHealth;

    public void Attack(Health targetHealth)
    {
        _lastTargetHealth = targetHealth;

        targetHealth.TakeDamage(_baseDamage);
    }
}
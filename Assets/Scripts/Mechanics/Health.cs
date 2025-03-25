using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public event Action TakedDamage;

    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeHealth(float health)
    {
        CurrentHealth += health;
        
        if (CurrentHealth >= _maxHealth)
            CurrentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            Die();

        TakedDamage?.Invoke();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
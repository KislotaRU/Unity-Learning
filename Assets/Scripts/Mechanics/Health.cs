using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public event Action Dead;

    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeHealth(float health)
    {
        CurrentHealth += health;
        Debug.Log($"��������� ������� +{health}. ����: {CurrentHealth}.  ���, ��� �������: {name}.");

        if (CurrentHealth >= _maxHealth)
            CurrentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"��������� ����� -{damage}. ����: {CurrentHealth}. ���, ��� �������: {name}.");

        if (CurrentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Dead?.Invoke();
    }
}
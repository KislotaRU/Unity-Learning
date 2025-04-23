using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damageValue;

    public int LastDamage { get; private set; }

    private void OnValidate()
    {
        _damageValue = _damageValue > 0 ? _damageValue : 0;
    }

    public void Attack(Health targetHealth)
    {
        targetHealth.TakeDamage(_damageValue);

        LastDamage = (int)targetHealth.ValueReduction;
    }
}
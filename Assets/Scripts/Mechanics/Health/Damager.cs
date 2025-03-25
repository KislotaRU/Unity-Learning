using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damageValue;

    public void Attack(Health targetHealth) =>
        targetHealth.TakeDamage(_damageValue);
}
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _baseDamage = 10;

    public void Attack(Health targetHealth) =>
        targetHealth.TakeDamage(_baseDamage);
}
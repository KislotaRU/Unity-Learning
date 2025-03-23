using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private int _baseHeal = 5;

    public void Heal(Health targetHealth) =>
        targetHealth.TakeHealth(_baseHeal);
}
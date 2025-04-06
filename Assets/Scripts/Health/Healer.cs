using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private int _healValue;

    public void Heal(Health targetHealth) =>
        targetHealth.TakeHealth(_healValue);
}
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private int _healValue;

    private void OnValidate()
    {
        _healValue = _healValue > 0 ? _healValue : 0;
    }

    public void Heal(Health targetHealth) =>
        targetHealth.TakeHealth(_healValue);
}
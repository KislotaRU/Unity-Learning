using UnityEngine;

public class BarUI : MonoBehaviour
{
    [field: SerializeField] public float CurrentValue { get; private set; }
    [field: SerializeField] public float MaxValue { get; private set; }
    public float PercentageValue => CurrentValue / MaxValue;
}
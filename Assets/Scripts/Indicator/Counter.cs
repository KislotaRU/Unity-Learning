using UnityEngine;

public class Counter : Indicator
{
    [SerializeField] private float _step;

    public void Increase() =>
        base.Increase(_step);

    public void Decrease() =>
        base.Decrease(_step);
}
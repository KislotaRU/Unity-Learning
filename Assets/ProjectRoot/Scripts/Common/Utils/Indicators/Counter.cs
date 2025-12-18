public class Counter : Countable, ICounter
{
    public Counter(float maxValue = 1f) : base(maxValue)
    {
    }

    public new void Increase(float value) =>
        base.Increase(value);

    public new void Decrease(float value) =>
        base.Decrease(value);
}
public class Health : Countable, IHealth
{
    public Health(float maxValue) : base(maxValue)
    {
    }

    public void TakeHealth(float health) =>
        Increase(health);

    public void TakeDamage(float damage) =>
        Decrease(damage);
}
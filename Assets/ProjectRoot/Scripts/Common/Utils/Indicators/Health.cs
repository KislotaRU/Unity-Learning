public class Health : Counter, IHealth
{
    public void TakeHealth(float health) =>
        Increase(health);

    public void TakeDamage(float damage) =>
        Decrease(damage);
}
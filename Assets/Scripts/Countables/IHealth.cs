public interface IHealth : ICountable
{
    void TakeHealth(float value);

    void TakeDamage(float value);
}
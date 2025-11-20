using System;

public interface ITimerService<T> : ITimerService
{
    void CreateTimer(T context, float duration, Action onComplete);

    void StopAll(T context);

    float GetAccumulatedTime(T context);
}

public interface ITimerService
{
    void Tick(float deltaTime);
}
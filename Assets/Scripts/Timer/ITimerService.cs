using System;

public interface ITimerService<T>
{
    void CreateTimer(T context, float duration, Action onComplete);

    void StopAll(T context);

    void Tick(float deltaTime);
}
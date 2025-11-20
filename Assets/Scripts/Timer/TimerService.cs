using System;
using System.Collections.Generic;
using System.Linq;

public class TimerService<T> : ITimerService<T>
{
    private readonly List<Timer> _timers;

    public TimerService()
    {
        _timers = new List<Timer>();
    }

    public void CreateTimer(T context, float duration, Action onCompleted)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        if (duration < 0)
            throw new ArgumentOutOfRangeException(nameof(duration));

        _timers.Add(new Timer(context, duration, onCompleted));
    }

    public void StopAll(T context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        _timers.RemoveAll(timer => timer.Context.Equals(context));
    }

    public float GetAccumulatedTime(T context)
    {
        return _timers.First(timer => timer.Context.Equals(context)).AccumulatedTime;
    }

    public void Tick(float deltaTime)
    {
        if (deltaTime < 0)
            throw new ArgumentOutOfRangeException(nameof(deltaTime));

        foreach (Timer timer in _timers)
        {
            timer.Increase(deltaTime);

            if (timer.IsCompleted)
            {
                _timers.Remove(timer);
                timer.OnCompleted?.Invoke();
            }
        }
    }

    private struct Timer
    {
        public float AccumulatedTime;
        public readonly float Duration;
        public readonly T Context;
        public readonly Action OnCompleted;

        public Timer(T context, float duration, Action onCompleted)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Duration = duration >= 0 ? duration : throw new ArgumentOutOfRangeException(nameof(duration));
            OnCompleted = onCompleted;
            AccumulatedTime = 0f;
        }

        public readonly bool IsCompleted => AccumulatedTime >= Duration;

        public void Increase(float deltaTime) =>
            AccumulatedTime += deltaTime > 0f ? deltaTime : throw new ArgumentOutOfRangeException(nameof(deltaTime));
    }
}
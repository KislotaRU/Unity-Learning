using System;
using System.Collections;
using UnityEngine;

public class Cooldown : Indicator
{
    [SerializeField, Min(1)] private int _reloadDuration;
    [SerializeField, Min(1)] private int _unloadDuration;

    private readonly WaitForSeconds _delay = new WaitForSeconds(1f);

    public event Action Unloaded;

    protected float ValuePerSecond { get; set; }

    protected Coroutine CurrentCoroutine;

    public void Reload()
    {
        if (CurrentCoroutine != null)
            StopCoroutine(CurrentCoroutine);

        CurrentCoroutine = StartCoroutine(Replenishing());
    }

    public void Unload()
    {
        if (CurrentCoroutine != null)
            StopCoroutine(CurrentCoroutine);

        CurrentCoroutine = StartCoroutine(Diminishing());
    }

    protected IEnumerator Replenishing()
    {
        ValuePerSecond = Mathf.RoundToInt(MaxValue / _reloadDuration);

        while (IsFull == false)
        {
            base.Increase(ValuePerSecond);

            yield return _delay;
        }
    }

    protected IEnumerator Diminishing()
    {
        ValuePerSecond = Mathf.RoundToInt(MaxValue / _unloadDuration);

        while (IsEmpty == false)
        {
            base.Decrease(ValuePerSecond);

            yield return _delay;
        }

        Unloaded?.Invoke();
    }
}
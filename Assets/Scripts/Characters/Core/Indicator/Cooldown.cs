using System.Collections;
using UnityEngine;

public class Cooldown : Indicator
{
    [SerializeField] protected float _durationPerSecond;
    [SerializeField] protected bool _isGradually;

    protected float ValuePerSecond { get; set; }

    protected Coroutine CurrentCoroutine;

    public void Replenish()
    {
        if (CurrentCoroutine == null)
            CurrentCoroutine = StartCoroutine(Replenishing());
    }

    public void Diminish()
    {
        if (_isGradually)
        {
            if (CurrentCoroutine == null)
                CurrentCoroutine = StartCoroutine(Diminishing());
        }
        else
        {
            base.Decrease(MaxValue);
        }
    }

    protected IEnumerator Replenishing()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);

        ValuePerSecond = Mathf.Round(MaxValue / _durationPerSecond);

        while (IsFull == false)
        {
            base.Increase(ValuePerSecond);

            yield return delay;
        }

        CurrentCoroutine = null;
    }

    protected IEnumerator Diminishing()
    {
        WaitForSeconds delay = new WaitForSeconds(1f);

        ValuePerSecond = Mathf.Round(MaxValue / _durationPerSecond);

        while (IsEmpty == false)
        {
            base.Decrease(ValuePerSecond);

            yield return delay;
        }

        CurrentCoroutine = null;
    }
}
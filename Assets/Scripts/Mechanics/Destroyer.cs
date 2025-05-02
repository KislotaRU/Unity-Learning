using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Destroyer : MonoBehaviour
{
    protected readonly float _minDelayDestroy = 2f;
    protected readonly float _maxDelayDestroy = 5f;

    public virtual event Action Destroyed;

    protected Coroutine _currentCoroutine;

    public void Destroy()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(Destroing());
    }

    protected virtual IEnumerator Destroing()
    {
        yield return new WaitForSeconds(Random.Range(_minDelayDestroy, _maxDelayDestroy));

        _currentCoroutine = null;

        Destroyed?.Invoke();
    }
}
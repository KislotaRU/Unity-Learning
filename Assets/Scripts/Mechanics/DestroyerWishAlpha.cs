using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class DestroyerWishAlpha : Destroyer
{
    public override event Action Destroyed;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    protected override IEnumerator Destroing()
    {
        float currentAlpha;
        float elapsedTime = 0f;
        float duration = Random.Range(_minDelayDestroy, _maxDelayDestroy);

        while (elapsedTime < duration)
        {
            currentAlpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            _renderer.material.color = new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, currentAlpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _currentCoroutine = null;

        Destroyed?.Invoke();
    }
}
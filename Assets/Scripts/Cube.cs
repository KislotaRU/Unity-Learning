using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Collider), typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private Painter _painter;

    private Color _defaultColor;
    private Renderer _renderer;
    private ObjectPool<Cube> _cubePool;

    private float _minDelayDestroy = 2f;
    private float _maxDelayDestroy = 5f;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        _defaultColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor _))
        {
            ChangeColor();
            StartCoroutine(nameof(DestroingWithDelay));
        }
    }

    private IEnumerator DestroingWithDelay()
    {
        yield return new WaitForSeconds(Random.Range(_minDelayDestroy, _maxDelayDestroy));

        _cubePool.Release(this);
    }

    public void Initialize(ObjectPool<Cube> cubePool)
    {
        _cubePool = cubePool;
    }

    private void ChangeColor()
    {
        if (_defaultColor == _renderer.material.color)
            _painter.Paint(_renderer);
    }
}
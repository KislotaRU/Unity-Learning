using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private Painter _painter;

    private readonly float _minDelayDestroy = 2f;
    private readonly float _maxDelayDestroy = 5f;

    private Color _defaultColor;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
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

        _cubeSpawner.ActionOnRelease(this);
    }

    public void ResetPosition(Vector3 position)
    {
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = position;
    }

    private void ChangeColor()
    {
        if (_defaultColor == _renderer.material.color)
            _painter.Paint(_renderer);
    }
}
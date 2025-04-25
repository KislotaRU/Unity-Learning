using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Painter _painter;

    public event Action<Cube> TouchedFloor;

    private readonly float _minDelayDestroy = 2f;
    private readonly float _maxDelayDestroy = 5f;

    private Rigidbody _rigidbody;

    private bool _isPainted = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor _) == false)
            return;

        if (_isPainted == false)
            HandlePaint();

        StartCoroutine(DestroingWithDelay());
    }

    public void ResetPosition(Vector3 position)
    {
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = position;
    }

    private IEnumerator DestroingWithDelay()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minDelayDestroy, _maxDelayDestroy));

        TouchedFloor?.Invoke(this);
    }

    private void HandlePaint()
    {
        _painter.Paint();

        _isPainted = true;
    }
}
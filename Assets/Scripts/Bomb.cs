using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] Detonator _detonator;

    private readonly float _minDelayExplode = 2f;
    private readonly float _maxDelayExplode = 5f;

    public event Action<Bomb> Exploded;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 position)
    {
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = position;

        StartCoroutine(DestroingWithDelay());
    }

    private IEnumerator DestroingWithDelay()
    {
        yield return new WaitForSeconds(Random.Range(_minDelayExplode, _maxDelayExplode));

        HandleExplosion();
    }

    private void HandleExplosion()
    {
        _detonator.Explode();
    }
}
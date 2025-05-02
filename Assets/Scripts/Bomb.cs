using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private Destroyer _destroyer;
    [SerializeField] private Detonator _detonator;

    public event Action<Bomb> Detonated;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _destroyer.Destroyed += HandleExplode;
        _detonator.Detonated += HandleDetonated;
    }

    private void OnDisable()
    {
        _destroyer.Destroyed -= HandleExplode;
        _detonator.Detonated -= HandleDetonated;
    }

    public void Initialize(Vector3 position)
    {
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = position;

        HandleDestroy();
    }

    public void HandleDestroy()
    {
        _destroyer.Destroy();
    }

    private void HandleExplode()
    {
        _detonator.Explode();
    }

    private void HandleDetonated()
    {
        Detonated?.Invoke(this);
    }
}
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _deactivationDelay = 3f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Vector3 _direction;

    public event Action<Bullet> ElapsedTimeDeactivation;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        StartCoroutine(DeactivatingWithDelay());
    }
    
    private IEnumerator DeactivatingWithDelay()
    {
        yield return new WaitForSeconds(_deactivationDelay);

        ElapsedTimeDeactivation?.Invoke(this);
    }

    public void Initialize(Vector3 position, Vector3 direction, float speed)
    {
        transform.position = position;
        transform.up = direction;
        _direction = direction;
        _speed = speed;
    }

    private void Move()
    {
        _rigidbody.velocity = _direction * _speed;
    }
}
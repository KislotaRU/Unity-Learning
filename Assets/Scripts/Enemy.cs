using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speedMovement = 5f;

    public event Action<Enemy> TouchedTarget;

    private Rigidbody _rigidbody;
    private Target _target;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_target != null)
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speedMovement * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Target _))
        {
            TouchedTarget?.Invoke(this);
        }
    }

    public void Initialize(Vector3 position, Target target)
    {
        _rigidbody.velocity = Vector3.zero;
        _target = target;
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }
}
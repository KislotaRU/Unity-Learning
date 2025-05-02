using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Painter _painter;
    [SerializeField] private Destroyer _destroyer;

    public event Action<Cube> Destroyed;
    public event Action<Vector3> BombSpawning;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _destroyer.Destroyed += HandleDestryed;
    }

    private void OnDisable()
    {
        _destroyer.Destroyed -= HandleDestryed;
    }

    public void Initialize(Vector3 position)
    {
        _rigidbody.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor _) == false)
            return;

        HandlePaint();

        HandleDestroy();
    }

    private void HandlePaint()
    {
        _painter.Paint();
    }

    private void HandleDestroy()
    {
        _destroyer.Destroy();
    }

    private void HandleDestryed()
    {
        Destroyed?.Invoke(this);
        BombSpawning?.Invoke(transform.position);
    }
}
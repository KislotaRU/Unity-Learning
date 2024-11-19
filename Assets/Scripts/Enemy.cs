using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speedMovement = 2f;
    [SerializeField] private Vector3 _direction = Vector3.zero;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 position = _direction * _speedMovement * Time.deltaTime;

        transform.Translate(position);
    }

    public void Initialize(Vector3 position, Vector3 direction)
    {
        _rigidbody.velocity = Vector3.zero;
        _direction = direction;
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }
}
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed;
    [SerializeField] private Flipper _flipper;

    private Vector2 _direction;

    public void Initialize(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        
        _flipper.Flip(direction);

        _direction = direction;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
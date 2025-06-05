using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Vector2 _direction;

    public void Initialize(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        _direction = direction;

        _spriteRenderer.flipX = direction.x > 0 ? true : false;
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
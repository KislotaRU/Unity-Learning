using UnityEngine;

public class DirectionBullet : MonoBehaviour
{
    [SerializeField] private Vector2 _direction;

    public Vector2 Direction => _direction;
}
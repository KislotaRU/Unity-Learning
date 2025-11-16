using UnityEngine;

public class Mover : MonoBehaviour, IMover
{
    private const float MinSqrMagnitude = 0.1f;

    [SerializeField] private float _moveSpeed;

    public float CurrentSpeed { get; private set; }

    public void HandleMove(Vector2 direction)
    {
        CurrentSpeed = direction.sqrMagnitude;

        if (direction.sqrMagnitude <= MinSqrMagnitude)
            return;

        Move(direction);
    }

    private void Move(Vector2 direction)
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(direction.x, 0f, direction.y) * scaledMoveSpeed;
        
        transform.Translate(offset);
    }
}
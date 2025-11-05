using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude <= 0.1f)
            return;

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(direction.x, 0f, direction.y) * scaledMoveSpeed;

        transform.Translate(offset);
    }
}
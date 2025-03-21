using UnityEngine;

public class Flipper : MonoBehaviour
{
    private readonly float _degreesRotation = 180;

    private Vector2 _lastDirection;

    public void Flip(Vector2 direction)
    {
        if (direction.x < 0)
            _lastDirection = new Vector2(0, _degreesRotation);
        else if (direction.x > 0)
            _lastDirection = Vector2.zero;

        transform.rotation = Quaternion.Euler(_lastDirection);
    }
}
using UnityEngine;

public class Flipper : MonoBehaviour
{
    private readonly float _degreesRotation = 180;

    private Vector2 _lastRotation;

    public void Flip(Vector2 direction)
    {
        if (direction.x < 0)
            _lastRotation = new Vector2(0, _degreesRotation);
        else if (direction.x > 0)
            _lastRotation = Vector2.zero;

        transform.rotation = Quaternion.Euler(_lastRotation);
    }
}
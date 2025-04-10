using UnityEngine;

public class Flipper : MonoBehaviour
{
    private readonly float _degreesRotation = 180;

    private Vector2 _lastRotation;

    public Vector2 FaceDirection => transform.rotation.y != 0 ? Vector2.left : Vector2.right;

    public void Flip(Vector2 direction)
    {
        if (direction.x < 0)
            _lastRotation = new Vector2(0, _degreesRotation);
        else if (direction.x > 0)
            _lastRotation = Vector2.zero;

        transform.rotation = Quaternion.Euler(_lastRotation);
    }
}
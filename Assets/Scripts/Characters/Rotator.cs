using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _lookSpeed;

    public void Look(Vector2 direction)
    {
        if (direction.sqrMagnitude <= 0.1f)
            return;

        float scaledLookSpeed = _lookSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(-direction.y, direction.x, 0f) * scaledLookSpeed;

        transform.Rotate(offset);
    }
}
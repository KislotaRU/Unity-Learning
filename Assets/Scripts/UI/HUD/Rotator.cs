using UnityEngine;

public class Rotator : MonoBehaviour
{
    private readonly float _degreesRotation = 0;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector2(0, _degreesRotation));
    }
}
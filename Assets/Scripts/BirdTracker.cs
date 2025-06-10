using UnityEngine;

public class BirdTracker : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private float _offsetX;

    private void LateUpdate()
    {
        Vector3 position = transform.position;

        position.x = _bird.transform.position.x + _offsetX;
        transform.position = position;
    }
}
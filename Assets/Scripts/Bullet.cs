using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void Initialize(Vector2 position, Vector2 rotation)
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector3 SpawnPosition { get; private set; }

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;

        SpawnPosition = position;
    }

    public void Move()
    {

    }

    public void Collect()
    {

    }
}
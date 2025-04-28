using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnZone : MonoBehaviour
{
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider>();

        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(_boxCollider.bounds.center, _boxCollider.bounds.size);
    }

    public Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(_boxCollider.bounds.min.x, _boxCollider.bounds.max.x);
        float positionY = Random.Range(_boxCollider.bounds.min.y, _boxCollider.bounds.max.y);
        float positionZ = Random.Range(_boxCollider.bounds.min.z, _boxCollider.bounds.max.z);

        return new Vector3(positionX, positionY, positionZ);
    }
}
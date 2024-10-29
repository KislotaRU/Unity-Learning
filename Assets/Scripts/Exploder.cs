using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField, Min(1.0f)] private float _explosionRadious = 40;
    [SerializeField, Min(1.0f)] private float _explosionForce = 600;

    public void Explode(List<Rigidbody> rigidbodies, Vector3 position)
    {
        foreach (Rigidbody explodableObject in rigidbodies)
            explodableObject.AddExplosionForce(_explosionForce, position, _explosionRadious);
    }
}
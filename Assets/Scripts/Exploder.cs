using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField, Min(1.0f)] private float _explosionRadious = 40;
    [SerializeField, Min(1.0f)] private float _explosionForce = 600;

    public void Explode(List<Rigidbody> rigidbodies)
    {
        foreach (Rigidbody explodableObject in rigidbodies)
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadious);
    }
}
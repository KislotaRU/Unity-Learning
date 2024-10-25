using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField, Min(1.0f)] private float _explosionRadious = 10;
    [SerializeField, Min(1.0f)] private float _explosionForce = 100;

    public void Explode(List<Rigidbody> rigidbodies)
    {
        foreach (Rigidbody explodableObject in rigidbodies)
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadious);
    }
}
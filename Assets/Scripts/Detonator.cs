using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _radius;

    public void Explode()
    {
        List<Rigidbody> rigidbodies = GetExplosiveObjects();

        foreach (Rigidbody explosiveObject in rigidbodies)
            explosiveObject.AddExplosionForce(_force, transform.position, _radius);
    }

    private List<Rigidbody> GetExplosiveObjects()
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                rigidbodies.Add(rigidbody);
        }

        return rigidbodies;
    }
}
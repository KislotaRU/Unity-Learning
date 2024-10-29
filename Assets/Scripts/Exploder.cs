using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)] private float _minExplosionRadious = 1f;
    [SerializeField, Range(1f, 50f)] private float _maxExplosionRadious = 50f;
    [Space]
    [SerializeField, Range(10f, 500f)] private float _minExplosionForce = 10f;
    [SerializeField, Range(10f, 500f)] private float _maxExplosionForce = 500f;

    private void OnValidate()
    {
        if (_minExplosionRadious >= _maxExplosionRadious)
            _minExplosionRadious = _maxExplosionRadious - 1;

        if (_minExplosionForce >= _maxExplosionForce)
            _minExplosionForce = _maxExplosionForce - 1;
    }

    public void Explode(Vector3 position, Vector3 scale)
    {
        List<Rigidbody> rigidbodies = GetExplodableObjects(position, scale, out float explosionForce, out float explosionRadious);

        ExplodeExecute(rigidbodies, explosionForce, position, explosionRadious);
    }

    private void ExplodeExecute(List<Rigidbody> rigidbodies, float explosionForce, Vector3 position, float explosionRadious)
    {
        foreach (Rigidbody explodableObject in rigidbodies)
            explodableObject.AddExplosionForce(explosionForce, position, explosionRadious);
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 position, Vector3 scale, out float explosionForce, out float explosionRadious)
    {
        List<Rigidbody> rigidbodies = new();

        int axesCount = 3;
        float coefficientExploder = (scale.x + scale.y + scale.z) / axesCount;
        coefficientExploder = Mathf.Clamp(coefficientExploder, 0f, 1f);

        explosionForce = Mathf.Lerp(_maxExplosionForce, _minExplosionForce, coefficientExploder);
        explosionRadious = Mathf.Lerp(_maxExplosionRadious, _minExplosionRadious, coefficientExploder);

        Collider[] colliders = Physics.OverlapSphere(position, explosionRadious);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                rigidbodies.Add(rigidbody);
        }

        return rigidbodies;
    }
} 
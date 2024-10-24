using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Exploder : MonoBehaviour
{
    [SerializeField, Min(1.0f)] private float _explosionRadious = 200;
    [SerializeField, Min(1.0f)] private float _explosionForce = 1000;

    private InputReader _inputReader;

    private void OnEnable()
    {
        _inputReader.Clicked += Explode;
    }

    private void OnDisable()
    {

        _inputReader.Clicked -= Explode;
    }

    private void Explode()
    {
        //foreach (Rigidbody explodableObject in _explodableObjects)
        //    explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadious);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Actions : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Cube _cube;
    [Space]
    [SerializeField, Range(0.0f, 1.0f)] private float _chanceClone = 1.0f;
    [SerializeField, Range(1, 100)] private int _reductionFactorChanceClone = 2;
    [SerializeField, Min(1)] private int _reductionFactorScale = 2;
    [Space]
    [SerializeField, Min(1.0f)] private float _explosionRadious = 200;
    [SerializeField, Min(1.0f)] private float _explosionForce = 1000;

    private List<Rigidbody> _explodableObjects;

    private void Start()
    {
        _explodableObjects = new List<Rigidbody>();
    }

    private void OnEnable()
    {
        _cube.MouseClicked += Clone;
        _cube.MouseClicked += DestroyObject;
    }

    private void OnDisable()
    {
        _cube.MouseClicked -= Clone;
        _cube.MouseClicked -= DestroyObject;
    }

    private void Clone()
    {
        GameObject gameObject;
        Rigidbody rigidbody;
        Renderer renderer;

        int minCountCubes = 2;
        int maxCountCubes = 6;
        int cubesCount = Random.Range(minCountCubes, maxCountCubes);

        float currentChanceClone = Random.value;

        if (_chanceClone < currentChanceClone)
            return;

        transform.localScale /= _reductionFactorScale;
        _chanceClone /= _reductionFactorChanceClone;

        for (int i = 0; i < cubesCount; i++)
        {
            gameObject = Instantiate(_prefab);
            
            renderer = gameObject.GetComponent<Renderer>();
            renderer.material.color = Random.ColorHSV();

            rigidbody = gameObject.GetComponent<Rigidbody>();
            _explodableObjects.Add(rigidbody);
        }
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in _explodableObjects)
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadious);
    }

    private void DestroyObject()
    {
        StartCoroutine(nameof(CoroutineDestroy));
    }

    private IEnumerator CoroutineDestroy()
    {
        float duration = 1f;
        float elapsedTime = 0f;
        float normalizedTime;
        Vector3 scaleStart = transform.localScale;
        Vector3 scaleEnd = Vector3.zero;

        Explode();

        while (elapsedTime <= duration)
        {
            normalizedTime = elapsedTime / duration;
            normalizedTime = Mathf.Clamp(normalizedTime, 0f, 1f);
            transform.localScale = Vector3.Lerp(scaleStart, scaleEnd, normalizedTime);
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
    
        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;
    [Space]
    [SerializeField, Range(0f, 1f)] private float _chanceToSpawn = 1.0f;
    [SerializeField, Min(1)] private int _reductionFactorChanceToSpawn = 2;
    [SerializeField, Min(1)] private int _reductionFactorScale = 2;

    private Cube _cube;

    private void Start()
    {
        _cube = GetComponent<Cube>();
    }

    public void Initialize()
    {
        Renderer renderer = GetComponent<Renderer>();

        renderer.material.color = Random.ColorHSV();
        _chanceToSpawn /= _reductionFactorChanceToSpawn;
        transform.localScale /= _reductionFactorScale;
    }

    private IEnumerator DestroyCoroutine()
    {
        float duration = 1f;
        float elapsedTime = 0f;
        float normalizedTime;
        Vector3 scaleStart = transform.localScale;
        Vector3 scaleEnd = Vector3.zero;

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

    public void OnCubeClicked()
    {
        DestroyObject();

        if (TrySpawn(out List<Cube> cubes) == false)
            return;

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Cube cube in cubes)
            rigidbodies.Add(cube.GetComponent<Rigidbody>());

        Explode(rigidbodies);
    }

    private void DestroyObject()
    {
        StartCoroutine(nameof(DestroyCoroutine));
    }

    private bool TrySpawn(out List<Cube> cubes)
    {
        float currentChanceClone = Random.value;
        cubes = null;

        if (_chanceToSpawn < currentChanceClone)
            return false;

        cubes = _spawner.Spawn(_cube, transform.position);

        return true;
    }

    private void Explode(List<Rigidbody> rigidbodies)
    {
        _exploder.Explode(rigidbodies);
    }
}
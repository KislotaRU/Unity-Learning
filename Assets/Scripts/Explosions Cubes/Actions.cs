using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Actions : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Cube _cube;
    [Space]
    [SerializeField, Range(0.0f, 1.0f)] private float _chanceClone = 1.0f;
    [SerializeField, Range(1, 100)] private int _reductionFactor = 2;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
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
        int minCountCubes = 2;
        int maxCountCubes = 6;
        int cubesCount = Random.Range(minCountCubes, maxCountCubes);

        float currentChanceClone = Random.value;

        if (_chanceClone < currentChanceClone)
            return;

        _renderer.material.color = Random.ColorHSV();

        for (int i = 0; i < cubesCount; i++)
            Instantiate(_prefab);

        _chanceClone /= _reductionFactor;
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
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private readonly int _poolCapacity = 5;
    private readonly int _poolMaxSize = 5;

    private readonly float _spawnDelay = 1f;

    private ObjectPool<Cube> _cubePool;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();

        _cubePool = new ObjectPool<Cube>(createFunc: () => Instantiate(_cubePrefab),
                                         actionOnGet: (cube) => GetPrefab(cube),
                                         actionOnRelease: (cube) => cube.gameObject.SetActive(false),
                                         actionOnDestroy: (cube) => Destroy(cube.gameObject),
                                         collectionCheck: true,
                                         defaultCapacity: _poolCapacity,
                                         maxSize: _poolMaxSize);
    }

    private void OnEnable()
    {
        _cubePrefab.TouchedFloor += OnRelease;
    }

    private void OnDisable()
    {
        _cubePrefab.TouchedFloor -= OnRelease;
    }

    private void Start()
    {
        StartCoroutine(SpawningWithDelay());
    }

    private IEnumerator SpawningWithDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            _cubePool.Get();

            yield return delay;
        }
    }

    private void OnRelease(Cube cube)
    {
        Debug.Log($"Обработка события");
        _cubePool?.Release(cube);
    }

    private void GetPrefab(Cube cube)
    {
        float coefficientAreaSpawner = 2;

        float randomPositionX = Random.Range(-_boxCollider.size.x / coefficientAreaSpawner, _boxCollider.size.x / coefficientAreaSpawner);
        float randomPositionZ = Random.Range(-_boxCollider.size.z / coefficientAreaSpawner, _boxCollider.size.z / coefficientAreaSpawner);

        Vector3 randomPosition = new Vector3(randomPositionX, transform.position.y, randomPositionZ);               

        cube.ResetPosition(randomPosition);

        cube.gameObject.SetActive(true);
    }
}
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

        _cubePool = new ObjectPool<Cube>(createFunc: () => CreateCube(),
                                         actionOnGet: (cube) => GetCube(cube),
                                         actionOnRelease: (cube) => cube.gameObject.SetActive(false),
                                         actionOnDestroy: (cube) => DestroyCube(cube),
                                         collectionCheck: true,
                                         defaultCapacity: _poolCapacity,
                                         maxSize: _poolMaxSize);
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
        _cubePool?.Release(cube);
    }

    private void GetCube(Cube cube)
    {
        float coefficientAreaSpawner = 2;

        float randomPositionX = Random.Range(-_boxCollider.size.x / coefficientAreaSpawner, _boxCollider.size.x / coefficientAreaSpawner) + transform.position.x;
        float randomPositionZ = Random.Range(-_boxCollider.size.z / coefficientAreaSpawner, _boxCollider.size.z / coefficientAreaSpawner) + transform.position.z;

        Vector3 randomPosition = new Vector3(randomPositionX, transform.position.y, randomPositionZ);               

        cube.ResetPosition(randomPosition);

        cube.gameObject.SetActive(true);
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(_cubePrefab);
        cube.TouchedFloor += OnRelease;
        return cube;
    }

    private void DestroyCube(Cube cube)
    {
        cube.TouchedFloor -= OnRelease;
        Destroy(cube.gameObject);
    }
}
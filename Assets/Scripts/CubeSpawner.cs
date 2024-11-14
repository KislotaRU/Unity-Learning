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
                                         actionOnGet: (cube) => ActionOnGet(cube),
                                         actionOnRelease: (cube) => cube.gameObject.SetActive(false),
                                         actionOnDestroy: (cube) => Destroy(cube),
                                         collectionCheck: true,
                                         defaultCapacity: _poolCapacity,
                                         maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(nameof(SpawningWithDelay));
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

    public void ActionOnRelease(Cube cube)
    {
        _cubePool?.Release(cube);
    }

    private void ActionOnGet(Cube cube)
    {
        float coefficientAreaSpawner = 2;

        float randomPositionX = Random.Range(-_boxCollider.size.x / coefficientAreaSpawner, _boxCollider.size.x / coefficientAreaSpawner);
        float randomPositionZ = Random.Range(-_boxCollider.size.z / coefficientAreaSpawner, _boxCollider.size.z / coefficientAreaSpawner);

        Vector3 randomPosition = new Vector3(randomPositionX, transform.position.y, randomPositionZ);               

        cube.ResetPosition(randomPosition);

        cube.gameObject.SetActive(true);
    }
}
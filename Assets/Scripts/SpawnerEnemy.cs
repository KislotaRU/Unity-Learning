using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider))]
public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Target _target;

    private readonly int _poolCapacity = 2;
    private readonly int _poolMaxSize = 2;

    private readonly float _spawnDelay = 1f;

    private ObjectPool<Enemy> _enemyPool;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();

        _enemyPool = new ObjectPool<Enemy>(createFunc: () => CreateObject(),
                                         actionOnGet: (obj) => GetObject(obj),
                                         actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                                         actionOnDestroy: (obj) => DestroyObject(obj),
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
            if (_enemyPool.CountActive < _poolMaxSize)
                _enemyPool.Get();

            yield return delay;
        }
    }

    private Enemy CreateObject()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.TouchedTarget += ReleaseObject;
        return enemy;
    }

    private void GetObject(Enemy enemy)
    {
        float coefficientAreaSpawner = 2;
        float randomPositionX = Random.Range(-_boxCollider.size.x / coefficientAreaSpawner, _boxCollider.size.x / coefficientAreaSpawner) + transform.position.x;
        float randomPositionZ = Random.Range(-_boxCollider.size.z / coefficientAreaSpawner, _boxCollider.size.z / coefficientAreaSpawner) + transform.position.z;
        Vector3 randomPosition = new Vector3(randomPositionX, transform.position.y, randomPositionZ);

        enemy.Initialize(randomPosition, _target);
        enemy.gameObject.SetActive(true);
    }

    private void ReleaseObject(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }

    private void DestroyObject(Enemy enemy)
    {
        enemy.TouchedTarget -= ReleaseObject;
        Destroy(enemy.gameObject);
    }
}
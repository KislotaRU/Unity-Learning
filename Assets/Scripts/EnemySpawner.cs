using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    private readonly int _poolCapacity = 5;
    private readonly int _poolMaxSize = 5;

    private readonly float _spawnDelay = 2f;

    private ObjectPool<Enemy> _enemyPool;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();

        _enemyPool = new ObjectPool<Enemy>(createFunc: () => CreateObject(),
                                         actionOnGet: (obj) => GetObject(obj),
                                         actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                                         actionOnDestroy: (obj) => Destroy(obj.gameObject),
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
            _enemyPool.Get();

            yield return delay;
        }
    }

    private void GetObject(Enemy enemy)
    {
        float coefficientAreaSpawner = 2;
        float randomPositionX = Random.Range(-_boxCollider.size.x / coefficientAreaSpawner, _boxCollider.size.x / coefficientAreaSpawner) + transform.position.x;
        float randomPositionZ = Random.Range(-_boxCollider.size.z / coefficientAreaSpawner, _boxCollider.size.z / coefficientAreaSpawner) + transform.position.z;
        Vector3 randomPosition = new Vector3(randomPositionX, transform.position.y, randomPositionZ);

        float coefficientDirection = 1;
        float randomDirectionX = Random.Range(-coefficientDirection, coefficientDirection);
        float randomDirectionY = Random.Range(-coefficientDirection, coefficientDirection);
        float randomDirectionZ = Random.Range(-coefficientDirection, coefficientDirection);
        Vector3 randomDirection = new Vector3(randomDirectionX, randomDirectionY, randomDirectionZ);

        enemy.Initialize(randomPosition, randomDirection);
        enemy.gameObject.SetActive(true);
    }

    private Enemy CreateObject()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        return enemy;
    }
}
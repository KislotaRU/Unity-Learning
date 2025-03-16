using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private SpawnZone _spawnZone;

    private readonly int _poolCapacity = 15;
    private readonly int _poolMaxSize = 15;

    private readonly bool _isSpawning = false;

    private float _delaySpawning = 3f;

    private ObjectPool<Item> _objectPool;

    private void Awake()
    {
        _objectPool = new ObjectPool<Item>(createFunc: () => CreateObject(),
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
        WaitForSeconds dealy = new WaitForSeconds(_delaySpawning);

        while (enabled)
        {
            if (_objectPool.CountActive < _poolMaxSize)
                _objectPool.Get();

            yield return dealy;
        }
    }

    private Item CreateObject()
    {
        Item item = Instantiate(_itemPrefab);
        item.ItemCollected += ReleaseObject;
        return item;
    }

    private void GetObject(Item item)
    {
        Vector2 position = _spawnZone.GetRandomPosition();

        item.Initialize(position);
        item.gameObject.SetActive(true);
    }

    private void ReleaseObject(Item item)
    {
        _objectPool.Release(item);
    }

    private void DestroyObject(Item item)
    {
        item.ItemCollected -= ReleaseObject;
        Destroy(item.gameObject);
    }
}
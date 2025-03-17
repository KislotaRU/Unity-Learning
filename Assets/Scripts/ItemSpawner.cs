using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private Spawn _spawn;

    [SerializeField, Range(1, 50)] private int _poolMaxSize = 30;
    [SerializeField, Range(1, 30)] private int _poolCapacity = 30;

    private readonly float _delaySpawning = 2f;

    private ObjectPool<Item> _itemPool;

    private void Awake()
    {
        _itemPool = new ObjectPool<Item>(createFunc: () => CreateObject(),
                                           actionOnGet: (obj) => GetObject(obj),
                                           actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                                           actionOnDestroy: (obj) => DestroyObject(obj),
                                           collectionCheck: true,
                                           defaultCapacity: _poolCapacity,
                                           maxSize: _poolMaxSize);
    }

    private void Start()
    {
        for (int i = 0; i < _poolCapacity; i++)
            if (_spawn.IsFreeSpawnZone)
                _itemPool.Get();

        StartCoroutine(SpawningWithDelay());
    }

    private IEnumerator SpawningWithDelay()
    {
        WaitForSeconds dealy = new WaitForSeconds(_delaySpawning);

        while (enabled)
        {
            if (_itemPool.CountActive < _poolMaxSize && _spawn.IsFreeSpawnZone)
                _itemPool.Get();

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
        Vector2 position = _spawn.GetRandomPosition();

        item.Initialize(_spawn.CurrentSpawnZone, position);
        item.gameObject.SetActive(true);
    }

    private void ReleaseObject(Item item)
    {
        _itemPool.Release(item);
    }

    private void DestroyObject(Item item)
    {
        item.ItemCollected -= ReleaseObject;
        Destroy(item.gameObject);
    }
}
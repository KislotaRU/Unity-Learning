using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Item> _items;

    [Header("Parameters spawner")]
    [SerializeField] private SpawnZoneStorage _spawnZoneStorage;
    [SerializeField, Range(1, 50)] private int _poolMaxSize = 30;
    [SerializeField, Range(1, 30)] private int _poolCapacity = 30;

    [Space]
    [SerializeField] private bool _isRespawning;

    private readonly float _delaySpawning = 2f;

    private ObjectPool<Item> _itemPool;

    private void OnValidate()
    {
        if (_poolCapacity > _poolMaxSize)
            _poolCapacity = _poolMaxSize;
    }

    private void Awake()
    {
        if (_items == null)
        {
            enabled = false;
            return;
        }

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
            if (_spawnZoneStorage.IsFreeSpawnZone)
                _itemPool.Get();

        if (_isRespawning)
            StartCoroutine(SpawningWithDelay());
    }

    private IEnumerator SpawningWithDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(_delaySpawning);

        while (enabled)
        {
            if (_itemPool.CountActive < _poolMaxSize)
            {
                if (_spawnZoneStorage.IsFreeSpawnZone)
                    _itemPool.Get();
                else
                    _spawnZoneStorage.RefreshSpawnZones();
            }

            yield return delay;
        }
    }

    private Item CreateObject()
    {
        int randomNumberItem = Random.Range(0, _items.Count);
        Item item = Instantiate(_items[randomNumberItem]);

        item.ItemCollected += ReleaseObject;

        return item;
    }

    private void GetObject(Item item)
    {
        Vector2 position = _spawnZoneStorage.GetRandomPosition();

        item.Initialize(_spawnZoneStorage.CurrentSpawnZone, position);
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
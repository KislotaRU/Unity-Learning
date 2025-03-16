using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item _item;
    //[SerializeField] private SpawnPoint
    
    private readonly int _poolCapacity = 15;
    private readonly int _poolMaxSize = 15;

    private readonly bool _isSpawning = false;

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
        while (enabled)
        {
            if (_objectPool.CountActive < _poolMaxSize)
                _objectPool.Get();

            yield return null;
        }
    }

    private Item CreateObject()
    {
        Item item = Instantiate(_item);
        item.ItemCollected += ReleaseObject;
        return item;
    }

    private void GetObject(Item item)
    {


        item.Initialize(transform.position);
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
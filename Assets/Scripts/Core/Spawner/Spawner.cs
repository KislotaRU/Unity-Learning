using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Main Parameters")]
    [SerializeField] protected T _prefab;
    [SerializeField] protected SpawnZoneStorage _spawnZoneStorage;
    [SerializeField] protected Transform _container;
    [Space]
    [SerializeField, Min(0)] protected int _maxSize;
    [SerializeField, Min(0)] protected int _capacity;

    [Header("AutoSpawning Parameters")]
    [SerializeField] protected bool _autoSpawning;
    [SerializeField, Min(0f)] protected float _delay;

    public event Action<T> Spawned;

    protected ObjectPool<T> _objectPool;

    private void OnValidate()
    {
        _capacity = _capacity > _maxSize ? _maxSize : _capacity;
    }

    protected virtual void Awake()
    {
        _objectPool = new ObjectPool<T>(createFunc: () => Create(),
                                        actionOnGet: (@object) => Get(@object),
                                        actionOnRelease: (@object) => Release(@object),
                                        actionOnDestroy: (@object) => Destroy(@object),
                                        collectionCheck: true,
                                        defaultCapacity: _capacity,
                                        maxSize: _maxSize);
    }

    protected virtual void Start()
    {
        for (int i = 0; i < _capacity; i++)
            Spawn();

        if (_autoSpawning)
            StartCoroutine(SpawningWithDelay());
    }

    public virtual T Spawn()
    {
        T @object = null;

        if (_objectPool.CountActive < _maxSize)
        {
            @object = _objectPool.Get();
            Spawned?.Invoke(@object);
        }

        return @object;
    }

    public void AddExistingObjectToPool(T existingObject)
    {
        if (existingObject == null)
            return;

        if (_objectPool.CountInactive >= _maxSize)
            return;

        if (_container != null)
            existingObject.transform.SetParent(_container);

        _objectPool.Release(existingObject);

        Spawn();
    }

    protected IEnumerator SpawningWithDelay()
    {
        WaitForSeconds delay = new (_delay);

        while (_autoSpawning)
        {
            yield return delay;

            Spawn();
        }
    }

    protected virtual T Create()
    {
        return Instantiate(_prefab);
    }

    protected virtual void Get(T @object)
    {
        @object.gameObject.SetActive(true);
    }

    protected void HandleRelease(T @object)
    {
        _objectPool?.Release(@object);
    }

    protected virtual void Release(T @object)
    {
        @object.gameObject.SetActive(false);
    }

    protected virtual void Destroy(T @object)
    {
        Destroy(@object.gameObject);
    }
}
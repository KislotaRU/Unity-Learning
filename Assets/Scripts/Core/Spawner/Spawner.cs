using System;
using System.Collections;
using UnityEngine;

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
        _objectPool = new ObjectPool<T>(create: () => Create(),
                                        get: (@object) => Get(@object),
                                        release: (@object) => Release(@object),
                                        destroy: (@object) => Destroy(@object),
                                        capacity: _capacity,
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

    public virtual bool Add(T @object)
    {
        bool isAdded = false;

        if (@object == null)
            return isAdded;

        isAdded = _objectPool.Add(@object);

        return isAdded;
    }

    public virtual bool Remove(T @object)
    {
        if (@object == null)
            return false;

        return _objectPool.Remove(@object);
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
        @object.transform.parent = _container;

        @object.gameObject.SetActive(false);
    }

    protected virtual void Destroy(T @object)
    {
        Destroy(@object.gameObject);
    }
}
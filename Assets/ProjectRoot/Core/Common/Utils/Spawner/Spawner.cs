using System;
using System.Collections;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Main Parameters")]
    [SerializeField] protected T _prefab;
    [SerializeField] protected Transform _container;
    [Space]
    [SerializeField, Min(0)] protected int _maxSize;
    [SerializeField, Min(0)] protected int _capacity;
    [SerializeField, Min(0f)] protected float _delay;
    [Space]
    [SerializeField] protected bool _autoSpawning;

    public event Action<T> Spawned;

    protected ObjectPool<T> _objectPool;

    private void OnValidate()
    {
        _capacity = _capacity > _maxSize ? _maxSize : _capacity;
    }

    private void Awake()
    {
        _objectPool = new ObjectPool<T>(functionCreate: () => Create(),
                                        actionGet: (@object) => Get(@object),
                                        actionRelease: (@object) => Release(@object),
                                        actionDestroy: (@object) => Destroy(@object),
                                        maxSize: _maxSize,
                                        capacity: _capacity);
    }

    private void Start()
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

    public virtual void Add(T @object)
    {
        if (@object == null)
            throw new ArgumentNullException(nameof(@object));

        _objectPool.Add(@object);
    }

    public virtual void Remove(T @object)
    {
        if (@object == null)
            throw new ArgumentNullException(nameof(@object));

        _objectPool.Remove(@object);
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
        @object.transform.parent = null;

        @object.gameObject.SetActive(true);
    }

    protected void HandleRelease(T @object)
    {
        _objectPool?.Release(@object);
    }

    protected virtual void Release(T @object)
    {
        @object.gameObject.SetActive(false);

        @object.transform.parent = _container;
    }

    protected virtual void Destroy(T @object)
    {
        Destroy(@object.gameObject);
    }
}
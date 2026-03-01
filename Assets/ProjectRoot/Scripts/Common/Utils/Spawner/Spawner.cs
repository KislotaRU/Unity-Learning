using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private T _prefab;
    [Space]
    [SerializeField, Min(0)] private int _maxSize;
    [SerializeField, Min(0)] private int _initialSize;

    public event Action<T> Spawned;

    private ObjectPool<T> _objectPool;

    private void OnValidate()
    {
        _initialSize = _initialSize > _maxSize ? _maxSize : _initialSize;
    }

    private void Awake()
    {
        _objectPool = new ObjectPool<T>(functionCreate: () => Create(),
                                        actionGet: (@object) => Get(@object),
                                        actionRelease: (@object) => Release(@object),
                                        actionDestroy: (@object) => Destroy(@object),
                                        maxSize: _maxSize,
                                        initialSize: _initialSize);
    }

    private void Start()
    {
        for (int i = 0; i < _initialSize; i++)
            Spawn();
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

    protected virtual void Get(T @object)
    {
        if (@object == null)
            throw new ArgumentNullException(nameof(@object));

        @object.gameObject.SetActive(true);
    }

    protected virtual void Release(T @object)
    {
        if (@object == null)
            throw new ArgumentNullException(nameof(@object));

        @object.gameObject.SetActive(false);
    }

    protected virtual void OnRelease(T @object)
    {
        if (@object == null)
            throw new ArgumentNullException(nameof(@object));

        _objectPool.Release(@object);
    }

    private T Create()
    {
        return Instantiate(_prefab);
    }

    private void Destroy(T @object)
    {
        if (@object == null)
            throw new ArgumentNullException(nameof(@object));

        Destroy(@object.gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] protected T _prefab;

    [Header("Parameters Spawn")]
    [SerializeField, Min(0f)] protected float _delay;
    [Space]
    [SerializeField] protected bool _autoSpawning;

    [Header("Parameters object pool")]
    [SerializeField, Min(0)] protected int _maxSize;
    [SerializeField, Min(0)] protected int _capacity;

    protected ObjectPool<T> _objectPool;

    private HashSet<T> _activeObjects;

    private void OnValidate()
    {
        if (_capacity > _maxSize)
            _capacity = _maxSize;
    }

    private void Awake()
    {
        _objectPool = new ObjectPool<T>(createFunc: () => Create(),
                                        actionOnGet: (obj) => Get(obj),
                                        actionOnRelease: (obj) => Release(obj),
                                        actionOnDestroy: (obj) => Destroy(obj),
                                        collectionCheck: true,
                                        defaultCapacity: _capacity,
                                        maxSize: _maxSize);

        _activeObjects = new HashSet<T>();
    }

    private void Start()
    {
        if (_autoSpawning)
            StartCoroutine(SpawningWithDelay());
    }

    public virtual void Spawn()
    {
        if (_objectPool.CountActive < _maxSize)
            _objectPool.Get();
    }

    public virtual void Reset()
    {
        foreach (var oneObject in _activeObjects.ToList())
        {
            if (oneObject != null)
                _objectPool.Release(oneObject);
        }

        _objectPool.Clear();
    }

    protected void HandleRelease(T obj)
    {
        _objectPool?.Release(obj);
    }

    protected IEnumerator SpawningWithDelay()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn();

            yield return delay;
        }
    }

    protected virtual T Create()
    {
        return Instantiate(_prefab);
    }

    protected virtual void Get(T obj)
    {
        _activeObjects.Add(obj);

        obj.gameObject.SetActive(true);
    }

    protected virtual void Release(T obj)
    {
        _activeObjects.Remove(obj);

        obj.gameObject.SetActive(false);
    }

    protected virtual void Destroy(T obj)
    {
        Destroy(obj.gameObject);
    }
}
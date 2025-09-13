using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Parameters Spawner")]
    [SerializeField] protected Transform _container;
    [Space]
    [SerializeField] protected T _prefab;
    [Space]
    [SerializeField, Min(0f)] protected float _delay;
    [Space]
    [SerializeField] protected bool _autoSpawning;

    [Header("Parameters ObjectPool")]
    [SerializeField, Min(0)] protected int _maxSize;
    [SerializeField, Min(0)] protected int _capacity;

    protected ObjectPool<T> _objectPool;

    private void OnValidate()
    {
        if (_capacity > _maxSize)
            _capacity = _maxSize;
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
        if (_autoSpawning)
            StartCoroutine(SpawningWithDelay());
    }

    public virtual void Spawn()
    {
        if (_objectPool.CountActive < _maxSize)
            _objectPool.Get();
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
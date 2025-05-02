using System.Collections;
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

    [Header("Counters")]
    [SerializeField] protected Counter _allCount;
    [SerializeField] protected Counter _createdCount;
    [SerializeField] protected Counter _countInScene;

    protected ObjectPool<T> _objectPool;

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
        _allCount.Increase();
        _createdCount.Increase();

        return Instantiate(_prefab);
    }

    protected virtual void Get(T obj)
    {
        _countInScene.Increase();

        obj.gameObject.SetActive(true);
    }

    protected virtual void Release(T obj)
    {
        _countInScene.Decrease();

        obj.gameObject.SetActive(false);
    }

    protected virtual void Destroy(T obj)
    {
        _allCount.Decrease();

        Destroy(obj.gameObject);
    }
}
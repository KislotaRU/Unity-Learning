using System;
using System.Collections.Generic;

public class ObjectPool<T> : IDisposable where T : class
{
    private readonly List<T> _pool;

    private readonly int _maxSize;

    private readonly Func<T> _functionCreate;

    private readonly Action<T> _actionGet;
    private readonly Action<T> _actionRelease;
    private readonly Action<T> _actionDestroy;

    public ObjectPool(Func<T> functionCreate, Action<T> actionGet, Action<T> actionRelease, Action<T> actionDestroy, int maxSize, int capacity)
    {
        _functionCreate = functionCreate ?? throw new ArgumentNullException(nameof(functionCreate));
        _maxSize = maxSize > 0 ? maxSize : throw new ArgumentOutOfRangeException(nameof(maxSize));

        if (capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        _actionGet = actionGet;
        _actionRelease = actionRelease;
        _actionDestroy = actionDestroy;

        _pool = new List<T>(capacity);
    }

    public int CountAll { get; private set; }
    public int CountActive => CountAll - CountInactive;
    public int CountInactive => _pool.Count;

    public T Get()
    {
        T @object;
        int index;

        if (CountInactive == 0)
        {
            @object = _functionCreate();

            CountAll++;
        }
        else
        {
            index = CountInactive - 1;
            @object = _pool[index];

            _pool.RemoveAt(index);
        }

        _actionGet?.Invoke(@object);

        return @object;
    }

    public void Release(T @object)
    {
        if (@object == null)
            throw new ArgumentNullException(nameof(@object));
    
        _actionRelease?.Invoke(@object);

        if (CountInactive < _maxSize)
        {
            _pool.Add(@object);

            return;
        }

        CountAll--;

        _actionDestroy?.Invoke(@object);
    }

    public void Add(T @object)
    {
        if (@object == null)
            throw new ArgumentNullException(nameof(@object));

        Release(@object);

        CountAll++;
    }

    public void Remove(T @object)
    {
        if (@object == null)
            throw new ArgumentNullException(nameof(@object));

        _pool.Remove(@object);

        CountAll--;
    }

    public void Clear()
    {
        _pool.Clear();

        CountAll = 0;
    }

    public void Dispose()
    {
        Clear();
    }
}
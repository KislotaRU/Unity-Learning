using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : class
{
    private readonly List<T> _pool;
    private readonly Func<T> _create;
    private readonly Action<T> _get;
    private readonly Action<T> _release;
    private readonly Action<T> _destroy;

    private readonly int _maxSize;

    public int CountAll { get; private set; }
    public int CountActive => CountAll - CountInactive;
    public int CountInactive => _pool.Count;

    public ObjectPool(Func<T> create, Action<T> get = null, Action<T> release = null, Action<T> destroy = null, int capacity = 10, int maxSize = 10000)
    {
        _pool = new List<T>(capacity);
        _maxSize = maxSize;

        _create = create;
        _get = get;
        _release = release;
        _destroy = destroy;
    }

    public T Get()
    {
        T @object;
        int index;

        if (CountInactive == 0)
        {
            @object = _create();

            CountAll++;
        }
        else
        {
            index = CountInactive - 1;
            @object = _pool[index];

            _pool.RemoveAt(index);
        }

        _get?.Invoke(@object);

        return @object;
    }

    public void Release(T @object)
    {
        _release?.Invoke(@object);

        if (CountInactive < _maxSize)
        {
            _pool.Add(@object);

            return;
        }

        CountAll--;

        _destroy?.Invoke(@object);
    }

    public void Add(T @object)
    {
        if (_pool.Contains(@object))
            return;

        Release(@object);

        CountAll++;
    }

    public bool Remove(T @object)
    {
        bool isRemoved = _pool.Remove(@object);

        if (isRemoved)
            CountAll--;

        return isRemoved;
    }

    public void Clear()
    {
        if (_destroy != null)
        {
            foreach (T item in _pool)
            {
                _destroy(item);
            }
        }

        _pool.Clear();
        CountAll = 0;
    }
}
using System.Collections;

namespace EnchantingGraph;

public sealed class FixedList<T> : IList<T>, IEquatable<FixedList<T>>
{
    private readonly T[] _items;

    public FixedList(int capacity, T defaultValue = default!)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        _items = new T[capacity];
        for (int i = 0; i < capacity; i++)
        {
            _items[i] = defaultValue;
        }
    }

    public static FixedList<T> From(IEnumerable<T> values)
    {
        List<T> valueList = values.ToList();
        FixedList<T> list = new(valueList.Count);

        int i = 0;
        foreach (T value in valueList)
        {
            list[i] = value;
            i++;
        }

        return list;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)_items).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_items).GetEnumerator();
    }

    [Obsolete("Not supported.")]
    public void Add(T item)
    {
        throw new NotSupportedException();
    }

    [Obsolete("Not supported.")]
    public void Clear()
    {
        throw new NotSupportedException();
    }

    public bool Contains(T item)
    {
        return _items.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    [Obsolete("Not supported.")]
    public bool Remove(T item)
    {
        throw new NotSupportedException();
    }

    public int Count => _items.Length;

    public bool IsReadOnly => _items.IsReadOnly;

    public int IndexOf(T item)
    {
        return _items.IndexOf(item);
    }

    [Obsolete("Not supported.")]
    public void Insert(int index, T item)
    {
        throw new NotSupportedException();
    }

    [Obsolete("Not supported.")]
    public void RemoveAt(int index)
    {
        throw new NotSupportedException();
    }

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    public bool Equals(FixedList<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _items.Equals(other._items);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is FixedList<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _items.GetHashCode();
    }
}
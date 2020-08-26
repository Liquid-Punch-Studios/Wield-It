using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CircularBuffer<T> : IList<T>, IReadOnlyList<T>
{
	private T[] buffer;
	private int cursor = 0;

	private static int Mod(int x, int mod)
	{
		return x < 0 ? (x % mod + mod) : (x % mod);
	}

	private static T GetCircular(T[] array, int offset, int length, int index)
	{
		if (length > array.Length)
			throw new ArgumentOutOfRangeException(nameof(length));

		return array[Mod(offset + index, length)];
	}

	private static T SetCircular(T[] array, int offset, int length, int index, T value)
	{
		if (length > array.Length)
			throw new ArgumentOutOfRangeException(nameof(length));

		return array[Mod(offset + index, length)] = value;
	}

	public CircularBuffer(int size)
	{
		if (size < 0)
			throw new ArgumentOutOfRangeException(nameof(size));

		buffer = new T[size];
	}

	public T this[int index]
	{
		get
		{
			if (index < 0 || index >= Count)
				throw new IndexOutOfRangeException("Index is out of range.");
			return GetCircular(buffer, cursor, Count, index);
		}
		set
		{
			if (index < 0 || index >= Count)
				throw new IndexOutOfRangeException("Index is out of range.");
			SetCircular(buffer, cursor, Count, index, value);
		}
	}

	public int Count { get; private set; } = 0;

	public bool IsReadOnly => false;

	public void Add(T item)
	{
		if (buffer.Length == 0)
		{
			return;
		}
		else if (Count < buffer.Length)
		{
			buffer[cursor++] = item;
			Count++;
		}
		else //if (Count == buffer.Length)
		{
			SetCircular(buffer, cursor, buffer.Length, cursor, item);
			cursor = Mod(cursor + 1, buffer.Length);
		}
	}

	public void Clear()
	{
		Count = 0;
	}

	public bool Contains(T item)
	{
		return buffer.Contains(item);
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		if (array == null)
			throw new ArgumentNullException(nameof(array));
		if (array.Length < Count + arrayIndex)
			throw new ArgumentOutOfRangeException(nameof(arrayIndex));

		for (int i = 0; i < Count; i++)
		{
			array[i + arrayIndex] = this[i];
		}
	}

	public IEnumerator<T> GetEnumerator()
	{
		for (int i = 0; i < Count; i++)
		{
			yield return this[i];
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	public int IndexOf(T item)
	{
		return Count > 0 ? Mod(Array.IndexOf(buffer, item) - cursor, Count) : -1;
	}

	public void Insert(int index, T item)
	{
		if (index < 0 || index > Count)
			throw new IndexOutOfRangeException("Index is out of range.");

		if (Count < buffer.Length)
			Count++;

		for (int i = Count - 1; i > index; i--)
			this[i] = this[i - 1];

		this[index] = item;
		cursor++;
	}

	public bool Remove(T item)
	{
		int index = this.IndexOf(item);
		if (index >= 0)
		{
			RemoveAt(index);
			return true;
		}
		else
			return false;
	}

	public void RemoveAt(int index)
	{
		if (index < 0 || index >= Count)
			throw new IndexOutOfRangeException("Index is out of range.");

		Count--;
		for (int i = index; i < Count; i++)
			this[i] = this[i + 1];
	}
}


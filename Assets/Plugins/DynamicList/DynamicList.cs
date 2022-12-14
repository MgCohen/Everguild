using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[System.Serializable]
public class DynamicList<T> : DynamicList, IEnumerable<T> where T : class
{
    [SerializeReference] private List<T> _list = new List<T>();

    public T this[int index] => _list[index];
    public int Count => _list.Count;

    public IEnumerator<T> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    public override Type ListType()
    {
        return typeof(T);
    }

    public override Type GetIndexType(int index)
    {
        return _list[index].GetType();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    public T1 Get<T1>(T element) where T1: class, T
    {
        if(element is T1)
        {
            return element as T1;
        }
        return default;
    }

    public T1 Get<T1>(int index) where T1: class, T
    {
        if(_list[index] is T1)
        {
            return _list[index] as T1;
        }

        return default;
    }

    public List<T1> GetAll<T1>() where T1: class, T
    {
        return _list.Where(e => e is T1).Select(e => e as T1).ToList();
    }

    public void Add(T element)
    {
        _list.Add(element);
    }

    public void Inser(T element, int index)
    {
        _list.Insert(index, element);
    }

    public int IndexOf(T element)
    {
        return _list.IndexOf(element);
    }

    public void Clear()
    {
        _list.Clear();
    }

    public void Remove(T element)
    {
        _list.Remove(element);
    }

    public void RemoveAt(int index)
    {
        _list.RemoveAt(index);
    }
}

public abstract class DynamicList
{
    public abstract Type ListType();

    public abstract Type GetIndexType(int index);
}
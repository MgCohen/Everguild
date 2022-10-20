using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoExtensions
{
    public static bool RemoveComponent<T>(this GameObject obj) where T: MonoBehaviour
    {
        T component = obj.GetComponent<T>();
        if (component)
        {
            MonoBehaviour.Destroy(component);
        }
        return component;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonobehavior<T> : MonoBehaviour where T : SingletonMonobehavior<T>
{
    static T instance;
    public static T GetInstance()
    {
        T result = null;
        try
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            result = (T)instance;
        }
        catch (System.InvalidCastException)
        {
            return null;
        }
        return result;
    }
    public static T GetInstance(bool forceAddInEditor = false)
    {
        T result = GetInstance();
        if (forceAddInEditor && result == null)
        {
            var newInstance = new GameObject(typeof(T).FullName);
            result = newInstance.AddComponent<T>();
        }
        return GetInstance();
    }
    protected virtual void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = (T)this;
        }

    }
}

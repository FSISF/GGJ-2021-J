using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton pattern with MonoBehaviour parent
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    return instance;
                }
                if (instance == null)
                {
                    GameObject Singleton = new GameObject(typeof(T).ToString());
                    instance = Singleton.AddComponent<T>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
}
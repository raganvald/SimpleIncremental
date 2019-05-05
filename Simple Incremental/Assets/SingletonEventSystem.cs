using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEventSystem : MonoBehaviour
{
    public static SingletonEventSystem instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}

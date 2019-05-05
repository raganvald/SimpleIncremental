using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* This class is used for pooling objects and reusing objects for performance
 * This works best when lots of the same object are spawned multiple times
 * such as projectiles from a gun */
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    public List<PrePooledObjects> prePooledObjects;
    private Dictionary<string, Queue<GameObject>> dict = null;

    [Serializable]
    public struct PrePooledObjects
    {
        public GameObject gameObject;
        public int count;
    }

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            dict = new Dictionary<string, Queue<GameObject>>();
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        List<GameObject> pooledObjects = new List<GameObject>();
        foreach(PrePooledObjects prePoolObj in prePooledObjects)
        {
            for(int i = 0; i<prePoolObj.count; i++)
            {
                pooledObjects.Add(GetPooledObject(prePoolObj.gameObject));
            }
        }

        foreach(GameObject go in pooledObjects)
        {
            go.SetActive(false);
        }
    }

    public GameObject GetPooledObject(GameObject go)
    {
        if (!dict.ContainsKey(go.name))
        {
            dict.Add(go.name, new Queue<GameObject>());
        }

        if (dict[go.name].Count > 0)
        {
            GameObject poolableObj = dict[go.name].Dequeue();
            if (poolableObj == null)
            {
                Debug.Log("Null object, Someone destroyed a poolable object");
                poolableObj = Instantiate(go);
            }
            PoolableObject po = poolableObj.GetComponent<PoolableObject>();
            if (po == null)
            {
                po = poolableObj.AddComponent<PoolableObject>();
            }
            po.prefab = go;
            return poolableObj;
        }
        else
        {
            GameObject newGo = Instantiate(go);
            PoolableObject po = newGo.GetComponent<PoolableObject>();
            if( po == null)
            {
                po = newGo.AddComponent<PoolableObject>();
            }
            po.prefab = go;
            return newGo;
        }

    }

    public void ReleasePooledObject(PoolableObject po)
    {
        if (!dict.ContainsKey(po.prefab.name))
        {
            dict.Add(po.prefab.name, new Queue<GameObject>());
        }
        dict[po.prefab.name].Enqueue(po.gameObject);
    }
}
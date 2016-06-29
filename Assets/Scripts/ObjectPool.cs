using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool current;
    GameObject objectForPool;
    public int pooledAmount = 20;
    public bool growth = true;

    public List<GameObject> listOfObjects;
    public float[] pooledAmounts;

    List<Rigidbody> objectsForPool;
    void Awake()
    {
        current = this;
    }
    

    ///////

    void Start()
    {
        objectsForPool = new List<Rigidbody>();
        for(int i = 0; i < listOfObjects.Count; i++)
        {
            objectForPool = listOfObjects[i];
            for (int x = 0; x < pooledAmounts[i]; x++)
            {
                GameObject obj = Instantiate(objectForPool) as GameObject;
                obj.SetActive(false);
                objectsForPool.Add(obj.GetComponent<Rigidbody>());
            }
        }
        

    }

    public GameObject getPooledObject(GameObject o)
    {
        for (int x = 0; x < objectsForPool.Count; x++)
        {
            if ((objectsForPool[x].name.Contains(o.name)) && !objectsForPool[x].gameObject.activeInHierarchy)
            {
                return objectsForPool[x].gameObject;
            }
        }
        if (growth)
        {
            GameObject obj = Instantiate(o) as GameObject;
            obj.SetActive(false);
            objectsForPool.Add(obj.GetComponent<Rigidbody>());
            return obj;
        }
        return null;
    }

    public Rigidbody getPooledObjectRigidBody(GameObject o)
    {
        for (int x = 0; x < objectsForPool.Count; x++)
        {
            if ((objectsForPool[x].name.Contains(o.name)) && !objectsForPool[x].gameObject.activeInHierarchy)
            {
                return objectsForPool[x];
            }
        }
        if (growth)
        {
            GameObject obj = Instantiate(o) as GameObject;
            obj.SetActive(false);
            objectsForPool.Add(obj.GetComponent<Rigidbody>());
            return obj.GetComponent<Rigidbody>();
        }
        return null;
    }


}

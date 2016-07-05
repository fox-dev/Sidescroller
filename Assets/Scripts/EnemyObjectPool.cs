using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyObjectPool : MonoBehaviour
{

    public static EnemyObjectPool current;
    GameObject objectForPool;
    public int pooledAmount = 20;
    public bool growth = true;

    public List<GameObject> listOfObjects;
    public float[] pooledAmounts;

    List<GameObject> objectsForPool;
    void Awake()
    {
        current = this;
        objectsForPool = new List<GameObject>();
        for (int i = 0; i < listOfObjects.Count; i++)
        {
            objectForPool = listOfObjects[i];
            for (int x = 0; x < pooledAmounts[i]; x++)
            {
                GameObject obj = Instantiate(objectForPool) as GameObject;
                obj.SetActive(false);
                obj.transform.parent = transform;
                objectsForPool.Add(obj);
            }
        }
    }


    //////

    public GameObject getPooledObject(GameObject o)
    {
        for (int x = 0; x < objectsForPool.Count; x++)
        {
            if ((objectsForPool[x].name.Contains(o.name)) && !objectsForPool[x].activeInHierarchy)
            {
                return objectsForPool[x];
            }
        }
        if (growth)
        {
            GameObject obj = Instantiate(o) as GameObject;
            obj.SetActive(false);
            obj.transform.parent = transform;
            objectsForPool.Add(obj);
            return obj;
        }
        return null;
    }

    


}

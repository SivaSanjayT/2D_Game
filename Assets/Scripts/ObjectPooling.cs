using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject OriginalObj;


    public List<GameObject> pool;


    public int PoolCount = 10;

    public static ObjectPooling instance;

    private void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < PoolCount ; i++)
        {
            GameObject Object = Instantiate(OriginalObj);
            Object.SetActive(false);
            pool.Add(Object);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject Object in pool)
        {
            if (!Object.activeInHierarchy)
            {
                Object.SetActive(true);
                return Object;
            }
        }


        Debug.Log("ENTERED NEW LOOPER");
        GameObject newObject = Instantiate(OriginalObj);
        newObject.SetActive(true);
        pool.Add(newObject);
        return newObject;
    }
    public void ReturnObject(GameObject Object)
    {
        Object.SetActive(false);
    }
}

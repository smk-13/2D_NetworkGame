using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int number;
    List<GameObject> objectPool = new List<GameObject>();
    int index;

    private void Awake()
    {
        for (int i = 0; i < number; i++)
        {
            CreateObject();
        }
    }

    GameObject CreateObject()
    {
        GameObject createdObject = Instantiate(prefab, transform);
        objectPool.Add(createdObject);
        return createdObject;
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[index].activeInHierarchy == false)
            {
                return objectPool[index];
            }

            index++;
            if (index >= objectPool.Count)
            {
                index = 0;
            }
        }

        return CreateObject();
    }


}

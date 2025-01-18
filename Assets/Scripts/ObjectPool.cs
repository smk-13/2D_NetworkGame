using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class ObjectPool : NetworkBehaviour
{

    [SerializeField] GameObject prefab;
    [SerializeField] int number;
    Queue<GameObject> objectPoolQueue = new Queue<GameObject>();


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        for (int i = 0; i < number; i++)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        GameObject poolObject = Instantiate(prefab, transform);
        NetworkObject poolObjectNetwork = poolObject.GetComponent<NetworkObject>();
        poolObjectNetwork.Spawn();
        objectPoolQueue.Enqueue(poolObject);
    }

    public GameObject GetObject()
    {
        GameObject poolObject = objectPoolQueue.Dequeue();
        objectPoolQueue.Enqueue(poolObject);
        return poolObject;
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject poolObject = GetObject();
        poolObject.transform.position = position;
        poolObject.transform.rotation = rotation;
        poolObject.SetActive(true);
        return poolObject;
    }







    /*
    [SerializeField] GameObject prefab;
    [SerializeField] int number;
    List<GameObject> objectPool = new List<GameObject>();
    int index;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        for (int i = 0; i < number; i++)
        {
            CreateObject();
        }
    }

    GameObject CreateObject()
    {

        GameObject poolObject = Instantiate(prefab, transform);
        objectPool.Add(poolObject);

        // network
        //NetworkObject poolObjectNetwork = poolObject.GetComponent<NetworkObject>();
        //poolObjectNetwork.Spawn();

        return poolObject;
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


    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject poolObject = GetObject();
        poolObject.transform.position = position;
        poolObject.transform.rotation = rotation;
        poolObject.SetActive(true);
        return poolObject;
    }
    */


}

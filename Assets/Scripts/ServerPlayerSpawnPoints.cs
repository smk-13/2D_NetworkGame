using UnityEngine;
using System.Collections.Generic;
using DesignPatterns.Singleton;

public class ServerPlayerSpawnPoints : Singleton<ServerPlayerSpawnPoints>
{
    [SerializeField] List<GameObject> m_SpawnPoints;

    public GameObject GetRandomSpawnPoint()
    {
        if (m_SpawnPoints.Count == 0)
            return null;
        return m_SpawnPoints[Random.Range(0, m_SpawnPoints.Count)];
    }
}

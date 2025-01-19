using System.Collections.Generic;
using UnityEngine;

public class ActivateAsteroids : MonoBehaviour
{

    [SerializeField] JoinMenu joinMenu;
    [SerializeField] List<GameObject> Asteroids = new List<GameObject>();

    private void OnEnable()
    {
        joinMenu.OnStartHost += OnStartHostChanged;
        joinMenu.OnStartClient += OnClientHostChanged;
    }

    private void OnDisable()
    {
        joinMenu.OnStartHost -= OnStartHostChanged;
        joinMenu.OnStartClient -= OnClientHostChanged;
    }

    private void OnStartHostChanged()
    {
        foreach (GameObject asteroid in Asteroids)
        {
            asteroid.SetActive(true);
        }
    }

    private void OnClientHostChanged()
    {
        foreach (GameObject asteroid in Asteroids)
        {
            asteroid.SetActive(true);
        }
    }

}

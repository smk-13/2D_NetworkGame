using System.Collections.Generic;
using UnityEngine;

public class ActivateOnStart : MonoBehaviour
{

    [SerializeField] JoinMenu joinMenu;
    [SerializeField] List<GameObject> startObjects = new List<GameObject>();

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
        foreach (GameObject obj in startObjects)
        {
            obj.SetActive(true);
        }
    }

    private void OnClientHostChanged()
    {
        foreach (GameObject obj in startObjects)
        {
            obj.SetActive(true);
        }
    }




}

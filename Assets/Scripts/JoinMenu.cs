using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class JoinMenu : MonoBehaviour
{
    [SerializeField] UIDocument UIDoc;
    Button hostButton;
    Button clientButton;
    Button quitButton;

    private void OnEnable()
    {
        hostButton = UIDoc.rootVisualElement.Q<Button>("HostButton");
        clientButton = UIDoc.rootVisualElement.Q<Button>("ClientButton");
        quitButton = UIDoc.rootVisualElement.Q<Button>("QuitButton");

        hostButton.clicked += OnHostButtonClicked;
        clientButton.clicked += OnClientButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;

    }

    private void OnDisable()
    {
        hostButton.clicked -= OnHostButtonClicked;
        clientButton.clicked -= OnClientButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }

    void OnHostButtonClicked()
    {
        NetworkManager.Singleton.StartHost();
        Hide();
    }

    void OnClientButtonClicked()
    {
        NetworkManager.Singleton.StartClient();
        Hide();
    }

    void OnQuitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    void Hide()
    {
        UIDoc.gameObject.SetActive(false);
    }




}

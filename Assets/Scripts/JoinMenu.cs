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

    public event Action OnStartHost;
    public event Action OnStartClient;

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
        OnStartHost?.Invoke();
    }

    void OnClientButtonClicked()
    {
        NetworkManager.Singleton.StartClient();
        Hide();
        OnStartClient?.Invoke();
    }

    void Hide()
    {
        UIDoc.gameObject.SetActive(false);
    }

    void OnQuitButtonClicked()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }



}

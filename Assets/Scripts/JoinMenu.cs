using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class JoinMenu : MonoBehaviour
{
    [SerializeField] UIDocument UIDoc;
    Button hostButton;
    Button clientButton;

    private void OnEnable()
    {
        hostButton = UIDoc.rootVisualElement.Q<Button>("HostButton");
        clientButton = UIDoc.rootVisualElement.Q<Button>("ClientButton");

        hostButton.clicked += OnHostButtonClicked;
        clientButton.clicked += OnClientButtonClicked;

    }

    private void OnDisable()
    {
        hostButton.clicked -= OnHostButtonClicked;
        clientButton.clicked -= OnClientButtonClicked;
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

    void Hide()
    {
        UIDoc.gameObject.SetActive(false);
    }

}

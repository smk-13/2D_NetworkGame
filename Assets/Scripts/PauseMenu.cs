using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] UIDocument UIDoc;
    Button resumeButton;
    Button quitButton;

    private void OnEnable()
    {
        resumeButton = UIDoc.rootVisualElement.Q<Button>("ResumeButton");
        quitButton = UIDoc.rootVisualElement.Q<Button>("QuitButton");

        resumeButton.clicked += OnResumeButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;

    }

    private void OnDisable()
    {
        resumeButton.clicked -= OnResumeButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }

    void OnResumeButtonClicked()
    {
        UIDoc.gameObject.SetActive(false);
    }

    void OnQuitButtonClicked()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    void OnPause()
    {
        UIDoc.gameObject.SetActive(true);
    }

}

using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject PauseUI;

    void OnPause()
    {
        PauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        PauseUI.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

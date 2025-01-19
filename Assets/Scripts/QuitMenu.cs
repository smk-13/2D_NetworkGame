using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class QuitMenu : NetworkBehaviour
{

    [SerializeField] GameObject QuitUI;

    void OnPause()
    {
        QuitUI.SetActive(!QuitUI.activeSelf);
    }

    public void ResumeGame()
    {
        QuitUI.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

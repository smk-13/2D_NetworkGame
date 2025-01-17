using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class ClientPlayerController : NetworkBehaviour
{

    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerInput playerInput;

    private void Awake()
    {
        playerController.enabled = false;
        playerInput.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsClient;
        if (!IsOwner)
        {
            enabled = false;
            playerController.enabled = false;
            playerInput.enabled = false;
            return;
        }

        playerController.enabled = true;
        playerInput.enabled = true;
    }
}

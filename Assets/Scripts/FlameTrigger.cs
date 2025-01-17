using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;


public class FlameTrigger : NetworkBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] PlayerController playerController;

    public NetworkVariable<Color> flameColor = new NetworkVariable<Color>(Color.white,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        flameColor.OnValueChanged += OnColorChanged;

        // turn off when spawning by setting alpha to 0
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        flameColor.OnValueChanged -= OnColorChanged;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    void OnMove(InputValue value)
    {
        if (IsOwner && spriteRenderer != null && playerController != null)
        {
            flameColor.Value = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, value.Get<float>());
        }
    }



    /*
    void OnMove(InputValue value)
    {

        if (IsOwner && spriteRenderer != null && playerController != null)
        {
            ShowFlameColorServerRpc(value.Get<float>());
        }

    }

    [Rpc(SendTo.Server)]
    private void ShowFlameColorServerRpc(float alpha)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
    }
    */




}

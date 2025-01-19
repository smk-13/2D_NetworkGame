using UnityEngine;
using Unity.Netcode;

public class ColorTrigger : NetworkBehaviour
{
    public NetworkVariable<Color> checkPointColor = new NetworkVariable<Color>(Color.white);
    [SerializeField] SpriteRenderer spriteRenderer;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        checkPointColor.OnValueChanged += OnColorChanged;
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();  // not working?!
    }


    public override void OnNetworkDespawn()
    {
        base.OnNetworkSpawn();
        checkPointColor.OnValueChanged -= OnColorChanged;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        spriteRenderer.color = newColor;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        NetworkObject networkObject = collision.GetComponent<NetworkObject>();
        if (IsClient && networkObject != null && networkObject.IsOwner)
        {
            ChangeColorServerRpc(networkObject.OwnerClientId);
        }
    }

    [Rpc(SendTo.Server)]
    private void ChangeColorServerRpc(ulong playerId)
    {
        // TO DO: connect with PlayerColor script
        checkPointColor.Value = (playerId % 2 == 0) ? new Color(1, 0, 0, 0.5f) : new Color(0, 0, 1, 0.5f);
    }

}

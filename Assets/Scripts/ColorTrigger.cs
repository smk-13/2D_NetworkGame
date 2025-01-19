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
        spriteRenderer.color = checkPointColor.Value;
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
            ChangeColorRpc(networkObject.OwnerClientId);
        }
    }

    [Rpc(SendTo.Server)]
    private void ChangeColorRpc(ulong playerId)
    {
        // TO DO: connect with PlayerColor script
        checkPointColor.Value = (playerId % 2 == 0) ? new Color(1, 0, 0, 0.5f) : new Color(0, 0, 1, 0.5f);
        NotifyEveryoneColorChangeRpc(playerId);
    }


    [Rpc(SendTo.ClientsAndHost)]
    private void NotifyEveryoneColorChangeRpc(ulong playerId)
    {
        Debug.Log($"Player {playerId + 1} has captured the check point.");
    }


}

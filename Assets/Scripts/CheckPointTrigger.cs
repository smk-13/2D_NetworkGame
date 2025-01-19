using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;
using System.Collections;

public class CheckPointTrigger : NetworkBehaviour
{
    public NetworkVariable<Color> checkPointColor = new NetworkVariable<Color>(Color.white);
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] UIDocument UIDoc;
    Label messages;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        checkPointColor.OnValueChanged += OnColorChanged;
        spriteRenderer.color = Color.white;
        messages = UIDoc.rootVisualElement.Q<Label>("Messages");
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
        NotifyEveryoneColorChangeRpc(playerId);
        checkPointColor.Value = (playerId % 2 == 0) ? new Color(1, 0, 0, 0.5f) : new Color(0, 0, 1, 0.5f);
        

    }


    [Rpc(SendTo.ClientsAndHost)]
    private void NotifyEveryoneColorChangeRpc(ulong playerId)
    {
        Debug.Log($"Player {playerId + 1} has captured the check point.");
        StopAllCoroutines();
        StartCoroutine(DisplayTextMessage($"Player {playerId + 1} has captured the check point.", 10f));
    }


    IEnumerator DisplayTextMessage(string myMessage, float duration)
    {
        messages.text = myMessage;
        yield return new WaitForSeconds(duration);
        messages.text = "";
    }


}

using UnityEngine;
using Unity.Netcode;

public class PlayerColor : NetworkBehaviour
{
    [SerializeField] Color playerOneColor = new Color(1, 0, 0, 0.5f);
    [SerializeField] Color playerTwoColor = new Color(0, 0, 1, 0.5f);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = (OwnerClientId % 2 == 0) ? playerOneColor : playerTwoColor;
    }

}

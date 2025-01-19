using UnityEngine;
using Unity.Netcode;
using System;

public class Damageable : NetworkBehaviour
{

    // TO DO: player network health should inherit from this

    public float maxHealth = 5f;

    private NetworkVariable<float> healthVar = new NetworkVariable<float>();

    public event Action<float> OnHealthChanged;

    public float Health { get => healthVar.Value; }


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            healthVar.Value = maxHealth;
        }

        healthVar.OnValueChanged += OnHealthValueChanged;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        healthVar.OnValueChanged -= OnHealthValueChanged;
    }

    private void OnHealthValueChanged(float oldValue, float newValue)
    {
        OnHealthChanged?.Invoke(newValue);
    }

    [Rpc(SendTo.Server)]
    public void TakeDamageRpc(float amount)
    {
        if (IsOwner)
        {
            healthVar.Value -= 1;
            if (Health <= 0)
            {
                DeathRpc();
            }
        }
    }

    [Rpc(SendTo.Server)]
    protected virtual void DeathRpc()
    {
        NetworkObject networkObject = GetComponent<NetworkObject>();
        networkObject.Despawn();
        Destroy(gameObject);
    }

}

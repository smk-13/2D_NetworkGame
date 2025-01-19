using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerNetworkHealth : NetworkBehaviour
{

    public float maxHealth = 5f;

    private NetworkVariable<float> healthVar = new NetworkVariable<float>(5f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    //private NetworkVariable<float> healthVar = new NetworkVariable<float>(5f);

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
        OnHealthChanged?.Invoke(newValue); // this is used to inform the health UI
    }

    private void OnTest()  // press T
    {
        if (IsOwner)
        {
            healthVar.Value -= 1;
            Debug.Log(Health);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsOwner)
        {
            healthVar.Value -= 1;
            Debug.Log(Health);
        }
            
    }


}

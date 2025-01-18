using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;
using UnityEditor;

public class PlayerHealthUI : MonoBehaviour
{

    [SerializeField] UIDocument UIDoc;
    Label m_HealthLabel;
    float maxHealth;

    Label m_HealthPoints;

    private void Start()
    {
;       NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        m_HealthLabel = UIDoc.rootVisualElement.Q<Label>("HealthLabel");
        m_HealthPoints = UIDoc.rootVisualElement.Q<Label>("HealthPoints");
    }

    void OnDestroy()
    {
        if (NetworkManager.Singleton)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }

    void OnClientConnected(ulong clientId)
    {
        if (IsLocalClient(clientId))
        {
            NetworkObject playerNetworkObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            PlayerNetworkHealth playerNetworkHealth = playerNetworkObject.GetComponent<PlayerNetworkHealth>();
            maxHealth = playerNetworkHealth.maxHealth;

            if (playerNetworkHealth)
            {
                OnPlayerHealthChanged(playerNetworkHealth.Health);
                playerNetworkHealth.OnHealthChanged += OnPlayerHealthChanged;
            }
        }
    }

    void OnClientDisconnected(ulong clientId)
    {
        if (IsLocalClient(clientId))
        {
            OnPlayerHealthChanged(-1f);
        }
    }

    private bool IsLocalClient(ulong clientId)
    {
        return NetworkManager.Singleton.LocalClientId == clientId;
    }

    private void OnPlayerHealthChanged(float newHealthValue)
    {
        m_HealthLabel.text = $"Health {newHealthValue}/{maxHealth}";

        string healthPoints = "";
        for(int i = 0 ; i < (int)newHealthValue; i++)
        {
            healthPoints += "^ ";
        }

        m_HealthPoints.text = healthPoints;

    }


}

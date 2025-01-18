using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public PlayerNetworkHealth playerNetworkHealth;
    public UIDocument UIDoc;

    private Label m_HealthLabel;


    private void Start()
    {
        playerNetworkHealth.OnHealthChanged += OnPlayerHealthChanged;
        m_HealthLabel = UIDoc.rootVisualElement.Q<Label>("HealthLabel");
        OnPlayerHealthChanged(playerNetworkHealth.Health);

    }


    void OnPlayerHealthChanged(float newHealthValue)
    {
        m_HealthLabel.text = $"{newHealthValue}/{playerNetworkHealth.maxHealth}";
    }

}
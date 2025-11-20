using TMPro;
using UnityEngine;

public class HealthUpdater : MonoBehaviour
{
    public void updateDisplay(int health, int maxHealth)
    {
        GetComponent<TextMeshProUGUI>().text = "HEALTH: " + health + "/" + maxHealth;
    }
}

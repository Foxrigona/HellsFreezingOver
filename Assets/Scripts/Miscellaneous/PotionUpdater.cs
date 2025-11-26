using TMPro;
using UnityEngine;

public class PotionUpdater : MonoBehaviour
{
    private TextMeshProUGUI textBox;

    private void Awake()
    {
        this.textBox = GetComponent<TextMeshProUGUI>();
    }

    public void updatePotionCount(int potionCount)
    {
        this.textBox.text = potionCount.ToString();
    }
}

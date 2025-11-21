using TMPro;
using UnityEngine;

public class DemonEssenceUpdater : MonoBehaviour
{
    TextMeshProUGUI text;

    public void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public void updateDemonEssence(int essenceCount)
    {
        text.text = "DEMON ESSENCE: " + essenceCount;
    }
}
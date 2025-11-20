using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityIconShower : MonoBehaviour
{
    [SerializeField] private int abilityDisplayNumber = 1;
    private Image imageComponent;

    private void Awake()
    {
        imageComponent = this.GetComponent<Image>();
        AbilityHandlerPlayer.abilityGained.AddListener(displayAbilities);
        AbilityHandler.abilityUsed.AddListener(putOnCooldown);
    }

    private void displayAbilities(int abilityNumber, Sprite sprite)
    {
        if(abilityNumber == abilityDisplayNumber) this.GetComponent<Image>().sprite = sprite;
    }

    private void putOnCooldown(int abilityNumber, bool isOnCooldown)
    {
        Debug.Log(abilityNumber + ":" + isOnCooldown);
        if (abilityDisplayNumber == abilityNumber)
            if (isOnCooldown) imageComponent.color = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, 0.2f);
            else imageComponent.color = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, 1f);

    }
}

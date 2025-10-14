using UnityEngine;

public class Ability : MonoBehaviour
{
    protected AbilityScriptableObject abilityInformation;
    protected SpriteRenderer spRenderer;
    public void setAbilityInformation(AbilityScriptableObject abilityInformation)
    {
        this.abilityInformation = abilityInformation;
        spRenderer = GetComponent<SpriteRenderer>();
        spRenderer.sprite = abilityInformation.abilitySprite;
    }
}

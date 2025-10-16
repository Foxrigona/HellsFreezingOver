using UnityEngine;

public class Ability : MonoBehaviour
{
    protected AbilityScriptableObject abilityInformation;
    protected SpriteRenderer spRenderer;
    protected ActorType userType;
    public void setAbilityInformation(AbilityScriptableObject abilityInformation, ActorType userType)
    {
        this.abilityInformation = abilityInformation;
        spRenderer = GetComponent<SpriteRenderer>();
        spRenderer.sprite = abilityInformation.abilitySprite;
        this.userType = userType;
    }
}

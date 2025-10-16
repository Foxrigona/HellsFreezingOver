using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Pickups/Abilities")]
public class AbilityScriptableObject : ScriptableObject
{
    [Header("Ability Description")]
    public string abilityName;
    public string abilityDescription;
    public AbilityTypes abilityType;

    [Header("Ability Visuals")]
    public Sprite abilitySprite;

    [Header("Ability Damage")]
    public int initialDamage;
    public int damageOverTime;
    public float tickTime;
    public float abilitySize = 1f;

    [Header("Ability Functionality")]
    public float speed;
    public float duration;
    public float cooldown;
}

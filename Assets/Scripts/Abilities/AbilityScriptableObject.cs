using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Pickups/Abilities")]
public class AbilityScriptableObject : ScriptableObject
{
    [SerializeField] private string abilityName;
    [SerializeField] private Sprite abilitySprite;
    [SerializeField] private AbilityTypes abilityType;

    [SerializeField] private int damage;
    [SerializeField] private float speed;

    [SerializeField] private float duration;

    [SerializeField] private string abilityDescription;
}

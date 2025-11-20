using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class AbilityHandler : MonoBehaviour
{
    [SerializeField] protected AbilityScriptableObject ability1;
    [SerializeField] protected AbilityScriptableObject ability2;
    [SerializeField] protected bool canUseAb1 = true;
    [SerializeField] protected bool canUseAb2 = true;

    [SerializeField] protected GameObject wallPrefab;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected GameObject staticAttackPrefab;

    public static UnityEvent<int, bool> abilityUsed = new UnityEvent<int, bool>();

    protected ActorType userType;
    protected void Start()
    {
        userType = GetComponent<Health>().getActorType();
    }

    protected void useAbility(AbilityScriptableObject ability, int abilityNumber,Vector2 userPosition, Vector2 targetPosition)
    {
        if (this is AbilityHandlerPlayer)
            abilityUsed.Invoke(abilityNumber, true);
        if (ability == null) return;
        if (ability.abilityType == AbilityTypes.Wall)
        {
            //TODO: Instantiate the wall object and do the thingymabobs
            GameObject wall = Instantiate(wallPrefab, targetPosition, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, Aimer.getAimDirection(userPosition, targetPosition))));
            wall.GetComponent<Wall>().setAbilityInformation(ability, userType);
        }
        else if (ability.abilityType == AbilityTypes.Projectile)
        {
            //TODO: Instantiate a projectile object and give it the thingymabob
            GameObject projectile = Instantiate(projectilePrefab, userPosition + Aimer.getAimDirection(userPosition, targetPosition) * 2, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, Aimer.getAimDirection(userPosition, targetPosition))));
            projectile.GetComponent<Projectile>().setAbilityInformation(ability, userType);
        }
        else if (ability.abilityType == AbilityTypes.StaticAttack)
        {
            //TODO: Instantiate a static attack thingamabob
            GameObject staticAttack = Instantiate(staticAttackPrefab, targetPosition, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, Aimer.getAimDirection(userPosition, targetPosition))));
            staticAttack.GetComponent<StaticAttack>().setAbilityInformation(ability, userType);
        }
        StartCoroutine(startCooldown(abilityNumber, ability));
    }

    public AbilityScriptableObject[] getAbilties()
    {
        return new AbilityScriptableObject[] { ability1, ability2 };
    }

    protected IEnumerator startCooldown(int abilityNum, AbilityScriptableObject abilityInformation)
    {
        yield return new WaitForSeconds(abilityInformation.cooldown);
        if (abilityNum == 1) canUseAb1 = true;
        else if (abilityNum == 2) canUseAb2 = true;
        abilityUsed.Invoke(abilityNum, false);
    }
}

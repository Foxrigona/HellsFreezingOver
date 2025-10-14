using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] private AbilityScriptableObject ability1;
    [SerializeField] private AbilityScriptableObject ability2;
    [SerializeField] private bool canUseAb1 = true;
    [SerializeField] private bool canUseAb2 = true;

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject staticAttackPrefab;

    private InputAction ability1InputAction;
    private InputAction ability2InputAction;
    private void Start()
    {
        ability1InputAction = InputSystem.actions.FindAction("Ability 1");
        ability2InputAction = InputSystem.actions.FindAction("Ability 2");
    }

    private void Update()
    {
        if (ability1InputAction.WasPressedThisFrame() && canUseAb1)
        {
            canUseAb1 = false;
            useAbility(ability1);
            StartCoroutine(startCooldown(1, ability1));
        }
        if (ability2InputAction.WasPressedThisFrame() && canUseAb2)
        {
            canUseAb2 = false;
            useAbility(ability2);
            StartCoroutine(startCooldown(2, ability2));
        }
    }

    private void useAbility(AbilityScriptableObject ability)
    {
        if (ability == null) return;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (ability.abilityType == AbilityTypes.Wall)
        {
            //TODO: Instantiate the wall object and do the thingymabobs
            GameObject wall = Instantiate(wallPrefab, mousePosition, Quaternion.Euler(0,0,Vector2.SignedAngle(Vector2.up,Aimer.getAimDirection(this.transform.position))));
            wall.GetComponent<Wall>().setAbilityInformation(ability);
        }
        else if(ability.abilityType == AbilityTypes.Projectile)
        {
            //TODO: Instantiate a projectile object and give it the thingymabob
            GameObject projectile = Instantiate(projectilePrefab, transform.position + (Vector3)Aimer.getAimDirection(transform.position) * 2, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, Aimer.getAimDirection(this.transform.position))));
            projectile.GetComponent<Projectile>().setAbilityInformation(ability);
        }
        else if(ability.abilityType == AbilityTypes.StaticAttack)
        {
            //TODO: Instantiate a static attack thingamabob
            GameObject staticAttack = Instantiate(staticAttackPrefab, mousePosition, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, Aimer.getAimDirection(this.transform.position))));
            staticAttack.GetComponent<StaticAttack>().setAbilityInformation(ability);
        }
    }

    private IEnumerator startCooldown(int abilityNum, AbilityScriptableObject abilityInformation)
    {
        yield return new WaitForSeconds(abilityInformation.cooldown);
        if(abilityNum == 1) canUseAb1 = true;
        else if(abilityNum == 2) canUseAb2 = true;
    }
}

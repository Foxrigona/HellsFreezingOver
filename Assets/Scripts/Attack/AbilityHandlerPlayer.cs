using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class AbilityHandlerPlayer : AbilityHandler
{
    private InputAction ability1InputAction;
    private InputAction ability2InputAction;

    public static UnityEvent<int, Sprite> abilityGained = new UnityEvent<int, Sprite>();
    new private void Start()
    {
        base.Start();
        ability1InputAction = InputSystem.actions.FindAction("Ability 1");
        ability2InputAction = InputSystem.actions.FindAction("Ability 2");
    }

    public void giveAbilities(AbilityScriptableObject ability1, AbilityScriptableObject ability2)
    {
        this.ability1 = ability1;
        this.ability2 = ability2;
        if(ability1 != null) abilityGained.Invoke(1, ability1.abilitySprite);
        if(ability2 != null) abilityGained.Invoke(2, ability2.abilitySprite);
    }

    private void Update()
    {
        if (ability1InputAction.WasPressedThisFrame() && canUseAb1)
        {
            canUseAb1 = false;
            useAbility(ability1, 1, transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }
        if (ability2InputAction.WasPressedThisFrame() && canUseAb2)
        {
            canUseAb2 = false;
            useAbility(ability2, 2, transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }
    }
}

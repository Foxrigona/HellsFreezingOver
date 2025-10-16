using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class AbilityHandlerPlayer : AbilityHandler
{
    private InputAction ability1InputAction;
    private InputAction ability2InputAction;
    new private void Start()
    {
        base.Start();
        ability1InputAction = InputSystem.actions.FindAction("Ability 1");
        ability2InputAction = InputSystem.actions.FindAction("Ability 2");
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

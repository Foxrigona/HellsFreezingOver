using UnityEngine;

public class WildRogueHealth : Health
{
    private ChoicePanelHandler panel;
    private bool hasDied = false;

    private void Start()
    {
        panel = FindFirstObjectByType<ChoicePanelHandler>();
    }
    override public void kill()
    {
        if (hasDied) Destroy(this.gameObject);

        panel.toggleChoicePanel(true);
        panel.setChoiceType(this);

        this.hasDied = true;
    }

    public void takeAction(int choice)
    {
        if (choice == 0) becomeAlly();
        else givePlayerAbilities();
    }

    private void givePlayerAbilities()
    {
        AbilityScriptableObject[] currentAbilities = GetComponent<AbilityHandler>().getAbilties();
        FindFirstObjectByType<AbilityHandlerPlayer>().giveAbilities(currentAbilities[0], currentAbilities[1]);
        Destroy(this.gameObject);
    }

    private void becomeAlly()
    {
        this.currentHealth = this.maxHealth;
        GetComponent<SideChanger>().changeSide(ActorType.Rebel);
        GetComponent<SpriteRenderer>().color = Color.green;
    }
}

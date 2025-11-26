using TMPro;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    [SerializeField] private int cardNum = 1;
    private MonoBehaviour caller;
    private TextMeshProUGUI textBox;
    private int choice;

    private void Awake()
    {
        this.textBox = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void setDecision(MonoBehaviour caller)
    {
        choice = Random.Range(0, 2);

        this.caller = caller;
        if (caller is UpgradeScroll)
        {
            if (this.cardNum == 1)
            {
                if (choice == 0) this.textBox.text = "Double bullet fire rate";
                else if (choice == 1) this.textBox.text = "Increase Movement Speed";
            }
            else if (this.cardNum == 2)
            {
                if (choice == 0) this.textBox.text = "Increase bullets shot per round by 1";
                else if (choice == 1) this.textBox.text = "Increase Dashing distance";
            }
        }

        else if(caller is WildRogueHealth)
        {
            if (this.cardNum == 1)
            {
                this.textBox.text = "Recruit Rogue as an Ally";
            }
            else if (this.cardNum == 2)
            {
                this.textBox.text = "Kill the Rogue and take their powers";
            }
        }

        if(caller is HealthScroll)
        {
            if(this.cardNum == 1)
            {
                this.textBox.text = "Get a health potion\n(Press F)";
            }

            if (this.cardNum == 2)
            {
                this.textBox.text = "Increase max health";
            }
        }
    }

    public void activateEffect()
    {
        if(caller is UpgradeScroll)
        {
            UpgradeScroll scroll = (UpgradeScroll) caller;
            scroll.triggerUpgrade(cardNum + this.choice * 2 - 1);
        }
        if(caller is WildRogueHealth)
        {
            WildRogueHealth rogue = (WildRogueHealth) caller;
            rogue.takeAction(cardNum - 1);
        }
        if(caller is HealthScroll)
        {
            HealthScroll scroll = (HealthScroll) caller;
            scroll.triggerEffect(cardNum - 1);
        }
        transform.parent.GetComponent<ChoicePanelHandler>().toggleChoicePanel(false);
    }
}

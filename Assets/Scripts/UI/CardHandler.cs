using TMPro;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    [SerializeField] private int cardNum = 1;
    private MonoBehaviour caller;
    private TextMeshProUGUI textBox;

    private void Awake()
    {
        this.textBox = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void setDecision(MonoBehaviour caller)
    {
        this.caller = caller;
        if (caller is UpgradeScroll)
        {
            if (this.cardNum == 1)
            {
                this.textBox.text = "THIS IS COOLDOWN";
            }
            else if (this.cardNum == 2)
            {
                this.textBox.text = "THIS IS BULLET RANGE";
            }
        }

        else if(caller is WildRogueHealth)
        {
            if (this.cardNum == 1)
            {
                this.textBox.text = "Recruit as ally";
            }
            else if (this.cardNum == 2)
            {
                this.textBox.text = "Kill and take powers";
            }
        }

        if(caller is HealthScroll)
        {
            if(this.cardNum == 1)
            {
                this.textBox.text = "Get a health potion";
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
            scroll.triggerUpgrade(cardNum - 1);
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

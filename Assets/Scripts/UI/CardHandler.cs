using TMPro;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    [SerializeField] private int cardNum = 1;
    private MonoBehaviour caller;
    public void setDecision(MonoBehaviour caller)
    {
        this.caller = caller;
        if (caller is UpgradeScroll)
        {
            if (this.cardNum == 1)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = "THIS IS COOLDOWN";
            }
            else if (this.cardNum == 2)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = "THIS IS BULLET RANGE";
            }
        }

        else if(caller is WildRogueHealth)
        {
            if (this.cardNum == 1)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = "Recruit as ally";
            }
            else if (this.cardNum == 2)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = "Kill and take powers";
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
        transform.parent.GetComponent<ChoicePanelHandler>().toggleChoicePanel(false);
    }
}

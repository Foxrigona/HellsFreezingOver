using UnityEngine;

public class UpgradeScroll : MonoBehaviour
{
    private int bulletIncreaseAmount = 1;
    private float timerDecrementAmount = 1.2f;
    ChoicePanelHandler panel;
    //private float variationPercentage = 0.5f;

    private void Start()
    {
        panel = FindFirstObjectByType<ChoicePanelHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<PlayerHealth>() == null) return;
        panel.toggleChoicePanel(true);
        panel.setChoiceType(this);
    }

    public void triggerUpgrade(int choice)
    {
        UpgradeHandler upgradeHandler = FindFirstObjectByType<UpgradeHandler>();
        if (choice == 1) upgradeHandler.bulletCountUpgrade.Invoke(bulletIncreaseAmount);
        else upgradeHandler.bulletCooldownUpgrade.Invoke(timerDecrementAmount);
        Destroy(this.gameObject);
    }
}

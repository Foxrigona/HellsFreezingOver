using UnityEngine;

public class UpgradeScroll : MonoBehaviour
{
    private int bulletIncreaseAmount = 1;
    private float timerDecrementAmount = 1.2f;
    [SerializeField] private float movementSpeedIncrement = 0.4f;
    [SerializeField] private float dashSpeedIncrement = 0.1f;
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
        Debug.Log(choice);
        UpgradeHandler upgradeHandler = FindFirstObjectByType<UpgradeHandler>();
        if (choice == 0) upgradeHandler.bulletCooldownUpgrade.Invoke(timerDecrementAmount);
        else if (choice == 1) upgradeHandler.bulletCountUpgrade.Invoke(bulletIncreaseAmount);
        else if (choice == 2) upgradeHandler.moveSpeedUpgrade.Invoke(this.movementSpeedIncrement);
        else if (choice == 3) upgradeHandler.dashSpeedUpgrade.Invoke(this.dashSpeedIncrement);
        Destroy(this.gameObject);
    }
}

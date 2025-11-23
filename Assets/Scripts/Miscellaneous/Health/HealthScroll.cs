using UnityEngine;

public class HealthScroll : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] private int maxHealthIncrement = 20;

    private void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    private void collect()
    {
        FindFirstObjectByType<ChoicePanelHandler>().toggleChoicePanel(true);
        FindFirstObjectByType<ChoicePanelHandler>().setChoiceType(this);
    }

    public void triggerEffect(int choice)
    {
        if (choice == 0)
            increasePotionCount(1);
        else if (choice == 1)
            increaseMaxHealth(this.maxHealthIncrement);

        Destroy(this.gameObject);
    }

    private void increaseMaxHealth(int increment)
    {
        playerHealth.setMaxHealth((int)playerHealth.getMaxHealth() + increment);
        playerHealth.recoverHealth(increment);
    }
    private void increasePotionCount(int amount)
    {
        this.playerHealth.givePotion(amount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>() == null) return;
        collect();
    }
}

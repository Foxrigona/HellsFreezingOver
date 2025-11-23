using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [SerializeField] private float damageCooldown = 1f;
    [SerializeField] private int potionCount = 0;

    private bool isInvincible = false;
    private PlayerVisualizer playerVisualizer;
    private PotionUpdater potionUpdater;
    private InputAction potionUseButton;

    public void Start()
    {
        this.potionUpdater = FindFirstObjectByType<PotionUpdater>();
        playerVisualizer = GetComponentInChildren<PlayerVisualizer>();
        healthUpdater.updateDisplay((int)this.currentHealth, (int)this.maxHealth);
        this.potionUseButton = InputSystem.actions.FindAction("Use Potion");
    }

    public void Update()
    {
        if(this.potionUseButton.WasPressedThisFrame()) this.usePotion();
    }

    public override void kill()
    {
        SceneManager.LoadScene(2);
    }

    public void givePotion(int amount)
    {
        this.potionCount += amount;
        this.potionUpdater.updatePotionCount(this.potionCount);
    }

    public void takePotion(int amount)
    {
        this.potionCount -= amount;
        this.potionUpdater.updatePotionCount(this.potionCount);
    }

    public override void recoverHealth(int amt)
    {
        base.recoverHealth(amt);
        this.healthUpdater.updateDisplay((int)this.currentHealth, (int)this.maxHealth);
    }

    private void usePotion()
    {
        if (this.potionCount <= 0) return;
        takePotion(1);
        Potion.recoverHealth(this);
        healthUpdater.updateDisplay((int)this.currentHealth, (int)this.maxHealth);
    }

    public override void decreaseHealth(float initialDamage, float tickDamage, float tickSpeed)
    {
        base.decreaseHealth(initialDamage, tickDamage, tickSpeed);
        healthUpdater.updateDisplay((int)this.currentHealth, (int)this.maxHealth);
    }

    public override void setMaxHealth(int newMaxHealth)
    {
        base.setMaxHealth(newMaxHealth);
        healthUpdater.updateDisplay((int)this.currentHealth, (int)this.maxHealth);
    }

    public override void decreaseHealth(float damage)
    {
        if (!isInvincible)
        {
            base.decreaseHealth(damage);
            StartCoroutine(startDamageCooldown());
            playerVisualizer.visualizeDamage();
            healthUpdater.updateDisplay((int)this.currentHealth, (int)this.maxHealth);
        }
            
    }

    private IEnumerator startDamageCooldown()
    {
        this.isInvincible = true;
        yield return new WaitForSeconds(damageCooldown);
        this.isInvincible = false;
    }
}

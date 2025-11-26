using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 10;
    [SerializeField] protected ActorType actorType;
    [SerializeField] protected EnemyStats stats;
    protected PlayerVisualizer playerVisualizer;
    protected float currentHealth;
    protected HealthUpdater healthUpdater;

    private void Awake()
    {
        if (stats != null) this.maxHealth = stats.health;
        currentHealth = maxHealth;
        if(this is PlayerHealth) healthUpdater = FindFirstObjectByType<HealthUpdater>();
        this.playerVisualizer = GetComponentInChildren<PlayerVisualizer>();
        Debug.Log(this.playerVisualizer == null);
    }

    public float getMaxHealth()
    {
        return this.maxHealth;
    }

    public virtual void recoverHealth(int amt)
    {
        this.currentHealth += amt;
        if(this.currentHealth > maxHealth) this.currentHealth = maxHealth;
    }

    public virtual void setMaxHealth(int newMaxHealth)
    {
        this.maxHealth = newMaxHealth;
    }

    public void OnEnable()
    {
        this.currentHealth = this.maxHealth;
    }

    public void setActorType(ActorType actorType)
    {
        this.actorType = actorType;
    }

    public ActorType getActorType()
    {
        return actorType;
    }

    public virtual void decreaseHealth(float damage)
    {
        currentHealth -= damage;
        if(this.playerVisualizer != null) this.playerVisualizer.visualizeDamage();
        if (currentHealth <= 0)
            kill();
    }

    public virtual void decreaseHealth(float initialDamage, float tickDamage, float tickSpeed)
    {
        decreaseHealth(initialDamage);
        if (tickDamage == 0 && tickSpeed == 0) return;
        StartCoroutine(dealTickDamage(tickDamage, tickSpeed));
    }

    public abstract void kill();

    private IEnumerator dealTickDamage(float tickDamage, float tickSpeed)
    {
        for(int i = 0; i < 4; i++)
        {
            decreaseHealth(tickDamage);
            yield return new WaitForSeconds(tickSpeed);
        }
    }
}
public enum ActorType
{
    Rebel,
    Enemy
}

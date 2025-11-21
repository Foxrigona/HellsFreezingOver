using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 10;
    [SerializeField] protected ActorType actorType;
    protected float currentHealth;
    protected HealthUpdater healthUpdater;

    private void Awake()
    {
        currentHealth = maxHealth;
        if(this is PlayerHealth) healthUpdater = FindFirstObjectByType<HealthUpdater>();
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
        if (this is PlayerHealth) healthUpdater.updateDisplay((int)this.currentHealth, (int)this.maxHealth);
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

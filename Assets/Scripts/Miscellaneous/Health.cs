using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private ActorType actorType;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public ActorType getActorType()
    {
        return actorType;
    }

    public void decreaseHealth(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            kill();
    }

    public void decreaseHealth(int initialDamage, int tickDamage, float tickSpeed)
    {
        decreaseHealth(initialDamage);
        if (tickDamage == 0 && tickSpeed == 0) return;
        StartCoroutine(dealTickDamage(tickDamage, tickSpeed));
    }

    public abstract void kill();

    private IEnumerator dealTickDamage(int tickDamage, float tickSpeed)
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

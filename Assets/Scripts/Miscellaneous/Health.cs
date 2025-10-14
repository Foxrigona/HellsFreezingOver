using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private ActorType actorType;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void decreaseHealth(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
            kill();
    }

    public void decreaseHealth(int initialDamage, int tickDamage, float tickSpeed)
    {
        decreaseHealth(initialDamage);
        if (tickDamage == 0 && tickSpeed == 0) return;
        StartCoroutine(dealTickDamage(tickDamage, tickSpeed));
    }

    public void kill()
    {
        Debug.Log("IM DEAD");
        Destroy(this.gameObject);
    }

    private IEnumerator dealTickDamage(int tickDamage, float tickSpeed)
    {
        for(int i = 0; i < 4; i++)
        {
            decreaseHealth(tickDamage);
            yield return new WaitForSeconds(tickSpeed);
        }
    }

    private enum ActorType
    {
        Rebel,
        Enemy
    }
}

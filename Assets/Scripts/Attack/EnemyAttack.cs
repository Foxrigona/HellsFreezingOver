using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    private float damage;
    private float hitDelay;
    private int demonEssenceValue;
    private bool isPaused = true;
    private Health targetHealth;
    private Health health;

    private void Awake()
    {
        this.damage = this.stats.damage + Random.Range(-0.5f, 2f);
        this.hitDelay = this.stats.hitDelay;
        this.demonEssenceValue = stats.demonEssenceValue;
        this.health = GetComponent<Health>();
        this.GetComponent<NavMeshAgent>().speed = stats.speed + Random.Range(-0.5f,2f);
    }

    private void OnEnable()
    {
        this.targetHealth = null;
        StartCoroutine(damagePlayer());
    }

    public void alterStats(int waveNumber)
    {
        this.health.setMaxHealth(this.stats.health + Mathf.FloorToInt(waveNumber * 1));
        Debug.Log(this.health.getMaxHealth());
        this.damage = stats.damage + waveNumber * 2;
        this.GetComponent<NavMeshAgent>().speed = stats.speed + waveNumber * 0.5f;
    }

    public int getDemonEssenceValue()
    {
        Debug.Log(this.demonEssenceValue);
        return this.demonEssenceValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health h = collision.transform.GetComponent<Health>();
        if (h != null && h.getActorType() == ActorType.Rebel)
        {
            this.targetHealth = h;
            this.isPaused = false;
            Debug.Log("TAKE THIS " + this.targetHealth.transform.name);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Health h = collision.transform.GetComponent<Health>();
        if (h != null && h.getActorType() == ActorType.Rebel)
            this.isPaused = true;
    }

    private IEnumerator damagePlayer()
    {
        while (true)
        {
            Debug.Log(this.targetHealth);
            while (this.isPaused) yield return null;
            this.targetHealth.decreaseHealth(this.damage);
            yield return new WaitForSeconds(this.hitDelay);
        }
    }


}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyStats stats;
    private float damage;
    private float hitDelay;
    private bool isPaused = true;
    private Health targetHealth;

    private void Start()
    {
        this.damage = this.stats.damage + Random.Range(-0.5f, 2f);
        this.hitDelay = this.stats.hitDelay;
        this.GetComponent<NavMeshAgent>().speed = stats.speed + Random.Range(-0.5f,2f);
        StartCoroutine(damagePlayer());
    }

    public void alterStats(int waveNumber)
    {
        this.damage += waveNumber*0.5f - 0.5f;
        this.GetComponent<NavMeshAgent>().speed += waveNumber * 1.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health h = collision.transform.GetComponent<Health>();
        if (h != null && h.getActorType() == ActorType.Rebel)
        {
            this.targetHealth = h;
            this.isPaused = false;
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
            while (this.isPaused) yield return null;
            this.targetHealth.decreaseHealth(this.damage);
            yield return new WaitForSeconds(this.hitDelay);
        }
    }


}

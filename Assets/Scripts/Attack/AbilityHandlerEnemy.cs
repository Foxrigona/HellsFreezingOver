using System.Drawing;
using UnityEngine;

public class AbilityHandlerEnemy : AbilityHandler
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask enemyLayer;
    private Transform target;
    private ActorType side;
    private void Start()
    {
        base.Start();

        canUseAb1 = false;
        canUseAb2 = false;

        StartCoroutine(startCooldown(1, ability1));
        StartCoroutine(startCooldown(2, ability2));

        target = FindFirstObjectByType<Movement>().transform;
        this.side = GetComponent<Health>().getActorType();
    }

    private void Update()
    {
        if ((canUseAb1 || canUseAb2) && this.side == ActorType.Rebel) this.target = FindTarget();
        if (canUseAb1 && target != null)
        {
            float size = ability1.abilitySize;
            canUseAb1 = false;
            useAbility(ability1, 1, transform.position, target.position + new Vector3(Random.Range(-size,size), Random.Range(-size, size)));
        }
        if (canUseAb2 && target != null)
        {
            float size = ability2.abilitySize;
            canUseAb2 = false;
            useAbility(ability2, 2, transform.position, target.position + new Vector3(Random.Range(-size, size), Random.Range(-size, size)));
        }
    }

    private Transform FindTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, this.detectionRadius, enemyLayer);
        if (enemies.Length == 0) return null;
        Transform closestEnemy = enemies[0].transform;
        float closestDistance = (closestEnemy.position - transform.position).sqrMagnitude;

        foreach (Collider2D enemy in enemies)
        {
            float distance = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestEnemy = enemy.transform;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
}

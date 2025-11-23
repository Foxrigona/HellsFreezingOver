using System.Collections;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Projectile : Ability
{
    [SerializeField] private List<Health> targets;
    [SerializeField] private BoxCollider2D collosionBox;
    [SerializeField] private CircleCollider2D hitbox;
    new public void setAbilityInformation(AbilityScriptableObject abilityInformation, ActorType userType)
    {
        base.setAbilityInformation(abilityInformation, userType);
        this.spRenderer.size = new Vector2(1, 1);
        hitbox.radius = abilityInformation.abilitySize;
    }
    private void Start()
    {
        StartCoroutine(destroyProjectile(abilityInformation.duration));
    }

    public void Update()
    {
        if (abilityInformation == null) return;
        moveProjectile();
    }

    private void moveProjectile()
    {
        transform.position += transform.up * this.abilityInformation.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hitbox.IsTouching(collision))
        {
            Health health = collision.GetComponent<Health>();
            if (health == null) return;
            if (!targets.Contains(health) && health.getActorType() != userType)
                targets.Add(health);
        }
        if (collosionBox.IsTouching(collision))
        {
            Health targetHealth = collision.transform.GetComponent<Health>();
            Projectile targetProjectile = collision.transform.GetComponent<Projectile>();
            if (targetProjectile == null && (targetHealth == null || targetHealth.getActorType() != this.userType))
            {
                Debug.Log("GETTING DESTROYED AGAINST" + collision.transform.name);
                foreach (Health enemy in targets)
                    enemy.decreaseHealth(abilityInformation.initialDamage, abilityInformation.damageOverTime, abilityInformation.tickTime);
                Destroy(this.gameObject);
            }
        }     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (health == null) return;
        if (targets.Contains(health))
            targets.Remove(health);
    }

    private IEnumerator destroyProjectile(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}

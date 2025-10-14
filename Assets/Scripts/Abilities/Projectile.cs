using System.Collections;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Projectile : Ability
{
    [SerializeField] private List<Health> targets;
    public void setAbilityInformation(AbilityScriptableObject abilityInformation)
    {
        base.setAbilityInformation(abilityInformation);
        this.spRenderer.size = new Vector2(1, 1);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (Health enemy in targets)
            enemy.decreaseHealth(abilityInformation.initialDamage, abilityInformation.damageOverTime, abilityInformation.tickTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (health == null) return;
        if (!targets.Contains(health))
            targets.Add(health);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (health == null) return;
        if (targets.Contains(health))
            targets.Remove(health);
    }
}

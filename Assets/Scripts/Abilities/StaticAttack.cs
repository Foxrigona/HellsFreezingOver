using System.Collections;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class StaticAttack : Ability
{
    private List<Health> target = new List<Health>();
    public void setAbilityInformation(AbilityScriptableObject abilityInformation, ActorType userType)
    {
        base.setAbilityInformation(abilityInformation, userType);
        spRenderer.size = new Vector2(abilityInformation.abilitySize, abilityInformation.abilitySize);
        GetComponent<CircleCollider2D>().radius = abilityInformation.abilitySize;
        StartCoroutine(destroyAttack());
    }

    public IEnumerator destroyAttack()
    {
        yield return new WaitForSeconds(this.abilityInformation.duration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.transform.GetComponent<Health>();
        if (health == null) return;
        if (!target.Contains(health) && health.getActorType() != this.userType)
        {
            health.decreaseHealth(abilityInformation.initialDamage, abilityInformation.damageOverTime, abilityInformation.tickTime);
            target.Add(health);
        }
    }
}

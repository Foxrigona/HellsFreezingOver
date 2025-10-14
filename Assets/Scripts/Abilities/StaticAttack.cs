using System.Collections;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class StaticAttack : Ability
{
    private List<Health> target = new List<Health>();
    public void setAbilityInformation(AbilityScriptableObject abilityInformation)
    {
        base.setAbilityInformation(abilityInformation);
        spRenderer.size = new Vector2(3, 3);
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
        if (!target.Contains(health))
        {
            health.decreaseHealth(abilityInformation.initialDamage, abilityInformation.damageOverTime, abilityInformation.tickTime);
            target.Add(health);
        }
    }
}

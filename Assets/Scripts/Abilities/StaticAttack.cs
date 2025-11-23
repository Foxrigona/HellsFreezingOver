using System.Collections;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class StaticAttack : Ability
{
    private List<Health> target = new List<Health>();
    private bool attackStarted = false;
    [SerializeField] private float timeTillStart = 0.5f;
    [SerializeField] private Sprite dangerSprite;
    new public void setAbilityInformation(AbilityScriptableObject abilityInformation, ActorType userType)
    {
        base.setAbilityInformation(abilityInformation, userType);
        spRenderer.size = new Vector2(abilityInformation.abilitySize, abilityInformation.abilitySize);
        GetComponent<CircleCollider2D>().radius = abilityInformation.abilitySize/2;
        StartCoroutine(startAttack());
    }

    public IEnumerator destroyAttack()
    {
        yield return new WaitForSeconds(this.abilityInformation.duration);
        Destroy(gameObject);
    }
    public IEnumerator startAttack()
    {
        this.spRenderer.sprite = dangerSprite;
        yield return new WaitForSeconds(timeTillStart);
        this.spRenderer.sprite = abilityInformation.abilitySprite;
        this.attackStarted = true;
        this.attackEnemiesInList();
        StartCoroutine(destroyAttack());
    }

    private void attackEnemiesInList()
    {
        foreach (Health health in target)
        {
            health.decreaseHealth(abilityInformation.initialDamage, abilityInformation.damageOverTime, abilityInformation.tickTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.transform.GetComponent<Health>();
        if (health == null) return;
        if (!target.Contains(health) && health.getActorType() != this.userType)
        {
            target.Add(health);
            if (!this.attackStarted) return;
            health.decreaseHealth(abilityInformation.initialDamage, abilityInformation.damageOverTime, abilityInformation.tickTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.attackStarted) return;
        Health health = collision.transform.GetComponent<Health>();
        if (health == null) return;
        if (target.Contains(health) && health.getActorType() != this.userType)
            target.Remove(health);

    }
}

using System.Collections;
using UnityEngine;

public class Wall : Ability
{
    public void setAbilityInformation(AbilityScriptableObject ability)
    {
        base.setAbilityInformation(ability);
        spRenderer.size = new Vector2(4, spRenderer.size.y);
        StartCoroutine(destroyWall());
    }
    private IEnumerator destroyWall()
    {
        yield return new WaitForSeconds(abilityInformation.duration);
        Destroy(gameObject);
    }
}

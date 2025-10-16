using System.Collections;
using UnityEngine;

public class Wall : Ability
{
    NavMeshBaker baker;
    public void Awake()
    {
        baker = FindFirstObjectByType<NavMeshBaker>();
        Debug.Log(GetComponent<Collider2D>());
    }
    public void setAbilityInformation(AbilityScriptableObject ability, ActorType userType)
    {
        base.setAbilityInformation(ability, userType);
        spRenderer.size = new Vector2(ability.abilitySize, spRenderer.size.y);
        StartCoroutine(destroyWall());
        baker.rebakeMesh.Invoke(GetComponent<Collider2D>(), true);
    }
    private IEnumerator destroyWall()
    {
        yield return new WaitForSeconds(abilityInformation.duration);
        baker.rebakeMesh.Invoke(GetComponent<Collider2D>(), false);
        Destroy(gameObject);
    }
}

using UnityEngine;

public class SideChanger : MonoBehaviour
{
    [SerializeField] private int rebelLayer = 8;
    [SerializeField] private int enemyLayer = 7;
    private void Awake()
    {
        changeSide(GetComponent<Health>().getActorType());
    }
    public void changeSide(ActorType actorType)
    {
        if (actorType == ActorType.Rebel) gameObject.layer = rebelLayer;
        else gameObject.layer = enemyLayer;
        GetComponent<Health>().setActorType(actorType);
        GetComponent<AbilityHandlerEnemy>().updateSide(actorType);
        GetComponent<NPCMovement>().setActorType();
    }
}

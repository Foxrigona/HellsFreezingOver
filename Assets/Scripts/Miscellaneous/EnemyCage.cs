using UnityEngine;

public class EnemyCage : EssenceCollector
{
    [SerializeField] private GameObject cagedEnemy;

    protected override void performAction()
    {
        Instantiate(cagedEnemy, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

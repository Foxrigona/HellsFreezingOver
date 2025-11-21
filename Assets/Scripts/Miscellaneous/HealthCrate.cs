using UnityEngine;

public class HealthCrate : EssenceCollector
{
    [SerializeField] GameObject healthScroll;
    protected override void performAction()
    {
        Instantiate(healthScroll, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

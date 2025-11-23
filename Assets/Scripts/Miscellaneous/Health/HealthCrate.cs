using UnityEngine;
using UnityEngine.Events;

public class HealthCrate : EssenceCollector
{
    [SerializeField] private GameObject healthScroll;
    [SerializeField] private int costIncrement;

    private static UnityEvent<int> costIncrease = new UnityEvent<int>();

    new private void Start()
    {
        base.Start();
        costIncrease.AddListener(incrementCost);
    }

    private void incrementCost(int increment)
    {
        this.essenceNeeded += increment;
        this.updateVisuals();
    }

    protected override void performAction()
    {
        costIncrease.Invoke(this.costIncrement);
        Instantiate(healthScroll, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

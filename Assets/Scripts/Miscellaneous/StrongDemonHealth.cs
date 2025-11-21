using UnityEngine;

public class StrongDemonHealth : Health
{
    [SerializeField] GameObject scroll;
    private bool scrollSpawned = false;
    private int demonEssenceValue;

    public void Start()
    {
        demonEssenceValue = GetComponent<EnemyAttack>().getDemonEssenceValue();
    }

    private void OnEnable()
    {
        this.scrollSpawned = false;
    }
    override public void kill()
    {
        if (this.scrollSpawned) return;
        FindFirstObjectByType<EnemySpawner>().changePool(this.transform, true);
        EnemyKillHandler.enemyKilled.Invoke(this.demonEssenceValue);
        this.scrollSpawned = true;
        Instantiate(scroll, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}

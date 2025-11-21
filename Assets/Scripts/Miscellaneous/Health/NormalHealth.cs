using UnityEngine;

public class NormalHealth :Health
{
    private int demonEssenceValue;

    public void Start()
    {
        demonEssenceValue = GetComponent<EnemyAttack>().getDemonEssenceValue();
    }

    

    public override void kill()
    {
        FindFirstObjectByType<EnemySpawner>().changePool(this.transform, true);
        EnemyKillHandler.enemyKilled.Invoke(this.demonEssenceValue);
        gameObject.SetActive(false);
    }
}

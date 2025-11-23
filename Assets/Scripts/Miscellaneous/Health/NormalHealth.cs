using UnityEngine;

public class NormalHealth :Health
{
    public override void kill()
    {
        FindFirstObjectByType<EnemySpawner>().changePool(this.transform, true);
        EnemyKillHandler.enemyKilled.Invoke(this.stats.demonEssenceValue);
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.Events;

public class EnemyKillHandler : MonoBehaviour
{
    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] private int demonEssenceCount = 0;
    private DemonEssenceUpdater demonEssenceUpdater;
    public static UnityEvent<int> enemyKilled = new UnityEvent<int>();

    public void Start()
    {
        this.demonEssenceUpdater = FindFirstObjectByType<DemonEssenceUpdater>();
        enemyKilled.AddListener(updateEnemyKills);
    }

    private void updateEnemyKills(int demonEssenceValue)
    {
        enemiesKilled++;
        incrementEssenceAmount(demonEssenceValue);
    }

    private void incrementEssenceAmount(int essenceAmount)
    {
        demonEssenceCount += essenceAmount;
        demonEssenceUpdater.updateDemonEssence(demonEssenceCount);
    }

    public int removeEssenceAmount(int amount)
    {
        //TODO Check if the amount is had
        int difference = this.demonEssenceCount - amount;
        if(difference >= 0)
        {
            incrementEssenceAmount(-amount);
            return amount;
        }
        else
        {
            int tempVal = this.demonEssenceCount;
            incrementEssenceAmount(-this.demonEssenceCount);
            return tempVal;
        }
    }
}

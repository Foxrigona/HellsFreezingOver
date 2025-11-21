using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyTypes = new List<GameObject>();
    [SerializeField] List<int> enemyTypeChance = new List<int>();
    [SerializeField] List<GameObject> rogueTypes = new List<GameObject>();

    [SerializeField] List<List<Transform>> deadEnemyPools = new List<List<Transform>>();
    [SerializeField] List<List<Transform>> livingEnemyPools = new List<List<Transform>>();
    [SerializeField] List<int> poolSizes = new List<int>();

    [SerializeField] private int waveNumber = 1;
    [SerializeField] private int bossWaveInterval = 3;
    [SerializeField] private int enemyIncrement = 5;
    [SerializeField] private int initialEnemycount = 10;
    [SerializeField] private int waveDelay = 10;
    [SerializeField] private int waveCount = 5;
    private int currentWave = 0;
    Transform player;

    private void Awake()
    {
        player = FindFirstObjectByType<Movement>().transform;
    }

    private void Start()
    {
        populateEnemyPools();
        StartCoroutine(startSpawning());
    }

    private void populateEnemyPools()
    {
        for(int i = 0; i < poolSizes.Count; i++)
        {
            deadEnemyPools.Add(new List<Transform>());
            livingEnemyPools.Add(new List<Transform>());
            for(int enemyCount = 0; enemyCount < poolSizes[i]; enemyCount++)
            {
                GameObject enemy = Instantiate(enemyTypes[i], Vector3.zero, Quaternion.identity);
                enemy.SetActive(false);
                deadEnemyPools[i].Add(enemy.transform);
            }
        }
    }

    private void spawnEnemies(int enemyCount)
    {
        float sum = 0;
        foreach (int chance in enemyTypeChance)
            sum += chance;

        int enemyType = 0;

        for (int i = 0; i < enemyCount; i++)
        {
            float ranNumber = Random.Range(0f,1f);
            float current = 0f;
            for (int x = 0; x < enemyTypeChance.Count; x++){
                if (ranNumber <= current + enemyTypeChance[x] / sum)
                {
                    enemyType = x;
                    break;
                }
                else current += enemyTypeChance[x] / sum;
            }

            //set the spawn side of the enemy
            int spawnDirX;
            if (Random.Range(0, 2) == 0) spawnDirX = 1;
            else spawnDirX = -1;

            int spawnDirY;
            if (Random.Range(0, 2) == 0) spawnDirY = 1;
            else spawnDirY = -1;

            Vector2 spawnPosition = player.position + new Vector3(Random.Range(5, 15) * spawnDirX, Random.Range(5, 15) * spawnDirY, 0);

            GameObject e = null;
            //Instantiate enemy
            if (enemyType == 0)
            {
                e = deadEnemyPools[0][0].gameObject;
            }
            else if (enemyType == 1)
            {
                e = deadEnemyPools[1][0].gameObject;
            }
            e.transform.position = spawnPosition;
            e.SetActive(true);
            this.changePool(e.transform, false);
            e.GetComponent<EnemyAttack>().alterStats(currentWave);
        }    
    }

    public void changePool(Transform enemy, bool isDead)
    {
        Health enemyType = enemy.GetComponent<Health>();
        int pool = 0;
        if (enemyType is NormalHealth) pool = 0;
        if (enemyType is StrongDemonHealth) pool = 1;
        if (isDead)
        {
            livingEnemyPools[pool].Remove(enemy);
            deadEnemyPools[pool].Add(enemy);
        }
        else
        {
            deadEnemyPools[pool].Remove(enemy);
            livingEnemyPools[pool].Add(enemy);
        }
    }
    private void spawnRogue(int i)
    {
        int spawnDirX;
        if (Random.Range(0, 2) == 0) spawnDirX = 1;
        else spawnDirX = -1;

        int spawnDirY;
        if (Random.Range(0, 2) == 0) spawnDirY = 1;
        else spawnDirY = -1;

        Vector2 spawnPosition = player.position + new Vector3(Random.Range(5, 15) * spawnDirX, Random.Range(5, 15) * spawnDirY, 0);

        Instantiate(rogueTypes[i], spawnPosition, Quaternion.identity);
    }

    private IEnumerator startSpawning()
    {
        for (int i = 0; i < waveCount; i++)
        {
            if(i%bossWaveInterval != 0 || i == 0)
                spawnEnemies(initialEnemycount + i * enemyIncrement);
            else
                spawnRogue(i/bossWaveInterval-1);
            waveNumber++;
            yield return new WaitForSeconds(waveDelay);
        }
        SceneManager.LoadScene(3);
    }
}

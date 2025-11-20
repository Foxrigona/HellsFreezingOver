using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyTypes = new List<GameObject>();
    [SerializeField] List<int> enemyTypeChance = new List<int>();
    [SerializeField] List<GameObject> rogueTypes = new List<GameObject>();

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
        StartCoroutine(startSpawning());
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

            //Instantiate enemy
            GameObject e = Instantiate(enemyTypes[enemyType], spawnPosition, Quaternion.identity);
            e.GetComponent<EnemyAttack>().alterStats(currentWave);
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

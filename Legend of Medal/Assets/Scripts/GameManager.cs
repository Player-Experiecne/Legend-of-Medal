using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameLevels gameLevels;
    public Transform[] spawnPoints;

    private int currentLevelIndex = 0;

    private void Start()
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        if (currentLevelIndex < gameLevels.Levels.Count)
        {
            Level currentLevel = gameLevels.Levels[currentLevelIndex];
            Debug.Log("Starting Level: " + currentLevel.LevelName);
            StartCoroutine(SpawnWaves(currentLevel.Waves));
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }

    private IEnumerator SpawnWaves(List<Level.Wave> waves)  
    {
        foreach (var wave in waves)
        {
            yield return StartCoroutine(SpawnEnemies(wave.enemies));
        }

        currentLevelIndex++;
        LoadNextLevel();
    }

    private IEnumerator SpawnEnemies(List<Level.EnemySpawnInfo> enemies)  
    {
        foreach (var enemyInfo in enemies)
        {
            for (int i = 0; i < enemyInfo.count; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyInfo.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(1f);
            }
        }

        yield return new WaitForSeconds(2f);
    }
}

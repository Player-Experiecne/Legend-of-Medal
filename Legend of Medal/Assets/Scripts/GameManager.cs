using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Level> levels; // 所有关卡
    public Transform[] spawnPoints; 
    private int currentLevelIndex = 0;
    private int currentWaveIndex = 0;

    private static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadNextLevel();
    }

    void LoadNextLevel()
    {
        if (currentLevelIndex < levels.Count)
        {
            Debug.Log("Loading level: " + levels[currentLevelIndex].levelName);
            StartCoroutine(StartWaves(levels[currentLevelIndex].waves));
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }

    IEnumerator StartWaves(List<Wave> waves)
    {
        for (currentWaveIndex = 0; currentWaveIndex < waves.Count; currentWaveIndex++)
        {
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }

        currentLevelIndex++;
        LoadNextLevel();
    }

    IEnumerator SpawnWave(Wave wave)
    {
        foreach (var enemyInfo in wave.enemySpawnInfos)
        {
            for (int i = 0; i < enemyInfo.count; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; 
                Instantiate(enemyInfo.enemyType.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(1f); 
            }
        }
    }
}

/*using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels; // 所有的关卡
    private EnemyWaveManager waveManager;
    private int currentLevelIndex = 0;

    void Start()
    {
        waveManager = GetComponent<EnemyWaveManager>();
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex < levels.Count)
        {
            Level currentLevel = levels[currentLevelIndex];
            Debug.Log("Loading level: " + currentLevel.levelName); // 输出当前关卡名称
            StartCoroutine(waveManager.StartWaves(currentLevel.waves));
            currentLevelIndex++;
        }
        else
        {
            Debug.Log("All levels completed!");
        }
    }
}
*/
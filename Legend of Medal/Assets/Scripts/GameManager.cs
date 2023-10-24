using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GameManager : MonoBehaviour
{
    public GameLevels gameLevels;
    public Transform[] spawnPoints;
    public List<Dictionary<string, float>> monsterData;

    private int currentLevelIndex = 0;

    private DataService dataService;

    private DataService monsterDB;

    string path = Path.Combine(Application.streamingAssetsPath, "Monster Type.csv");

    public List<Dictionary<string, string>> ConvertCsvToDictList(string csvFilePath)
    {
        List<Dictionary<string, string>> dictList = new List<Dictionary<string, string>>();

        if (System.IO.File.Exists(csvFilePath))
        {
            string csvContent = System.IO.File.ReadAllText(csvFilePath);
            List<string[]> parsedData = CSVParser.Parse(csvContent);

            if (parsedData.Count > 0)
            {
                // 第一行是表头
                string[] headers = parsedData[0];

                for (int i = 1; i < parsedData.Count; i++) // 跳过表头
                {
                    Dictionary<string, string> rowDict = new Dictionary<string, string>();
                    string[] row = parsedData[i];

                    // 防止数据行的列数与表头不匹配
                    int colCount = Mathf.Min(headers.Length, row.Length);

                    for (int j = 0; j < colCount; j++)
                    {
                        rowDict[headers[j]] = row[j];
                    }

                    dictList.Add(rowDict);
                }
            }
        }
        else
        {
            Debug.LogError("CSV file does not exist: " + csvFilePath);
        }

        return dictList;
    }
    private void PrintDictList(List<Dictionary<string, string>> dictList)
    {
        foreach (var dict in dictList)
        {
            Debug.Log("==== New Entry ====");
            foreach (var pair in dict)
            {
                Debug.Log(pair.Key + ": " + pair.Value);
            }
        }
    }

    void Start()
    {
        // 创建 DataService 实例
        dataService = new DataService("PlayerSaves.db");

        monsterDB = new DataService("Monsters.db");



        // 创建一个新的玩家存档
        dataService.CreatePlayerSave("Revolt", 1, 200);

        monsterDB.ImportMonstersFromCSV(path);
        //onsterDB.ClearMonsterDB();

        //dataService.ClearDB();

        // 获取所有玩家存档
        IEnumerable<Monster> monsters = monsterDB.GetAllMonsters();
        /*   foreach (var monster in monsters)
           {
               Debug.Log(monster.ToString());
           }*/
        //存原始数据到rawData
        List<Dictionary<string, string>> rawData = ConvertCsvToDictList(path);

        //把string Dictionary解析成<int, float>
        monsterData = ConvertData(rawData);

        //PrintDictList(rawData);

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

    public List<Dictionary<string, float>> ConvertData(List<Dictionary<string, string>> originalData)
    {
        List<Dictionary<string, float>> convertedData = new List<Dictionary<string, float>>();

        foreach (var dictionary in originalData)
        {
            Dictionary<string, float> newDictionary = new Dictionary<string, float>();

            foreach (var entry in dictionary)
            {
                float value;

                // Try to parse the value or set to 0 if failed
                if (!float.TryParse(entry.Value, out value))
                {
                    value = 0f;
                }

                newDictionary.Add(entry.Key, value);
            }

            convertedData.Add(newDictionary);
        }

        return convertedData;
    }
}

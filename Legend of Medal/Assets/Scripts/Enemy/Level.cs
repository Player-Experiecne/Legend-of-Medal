using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] public int maxDefenders;
    [SerializeField] public int extraDefenderAfterEachWave;
    [SerializeField] private List<Wave> waves;

    public enum GeneTypeA
    {
        Null, ADom, AHet, ARec
    }

    public string LevelName => levelName;
    public List<Wave> Waves => waves;  

    [System.Serializable]
    public class Wave
    {
        [SerializeField] public List<EnemySpawnInfo> enemies;
    }

    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public int spawnLocation;
        public int count;
        public GeneTypeA geneTypeA = GeneTypeA.Null;
    }
}

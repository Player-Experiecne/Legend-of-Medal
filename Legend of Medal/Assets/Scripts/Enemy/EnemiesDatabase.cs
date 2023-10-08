using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Enemies Database", menuName = "Enemies Database")]
public class EnemiesDatabase : ScriptableObject
{
    public List<EnemyType> enemies; // 存储所有的敌人类型

    public GameObject GetEnemyPrefab(string name)
    {
        return enemies.Find(e => e.enemyName == name)?.enemyPrefab;
    }
}

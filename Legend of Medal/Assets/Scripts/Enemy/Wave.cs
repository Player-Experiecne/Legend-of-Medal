using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public List<EnemySpawnInfo> enemySpawnInfos; // 每波中不同类型的敌人及其数量
}

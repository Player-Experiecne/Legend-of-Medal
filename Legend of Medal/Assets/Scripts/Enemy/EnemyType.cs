using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy Type")]
public class EnemyType : ScriptableObject
{
    public GameObject enemyPrefab; // 敌人预制体
    public string enemyName; // 敌人名字或类型
}


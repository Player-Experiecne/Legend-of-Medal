using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public string levelName; // 关卡名称
    public List<Wave> waves; // 每关的所有波次
}

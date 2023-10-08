using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameLevels", menuName = "Game Levels", order = 52)]
public class GameLevels : ScriptableObject
{
    [SerializeField] private List<Level> levels;

    public List<Level> Levels => levels;
}

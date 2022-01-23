using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A manager used to hold all necessary resources needed for the game.
/// This includes sprites, scriptable objects, prefabs, etc. 
/// Basically anything that we want to load from the Resources folder.
/// </summary> 
public class ResourceManager : Singleton<ResourceManager>
{
    // Contains all recipes in the game
    public LevelData[] Levels;
    public Dictionary<string, LevelData> LevelDictionary;

    private void Awake()
    {
        LoadLevels();
    }

    void LoadLevels()
    {
        Levels = Resources.LoadAll<LevelData>("Levels");
        LevelDictionary = new Dictionary<string, LevelData>();

        foreach (LevelData level in Levels)
        {
            LevelDictionary[level.levelName] = level;
        }
    }
}
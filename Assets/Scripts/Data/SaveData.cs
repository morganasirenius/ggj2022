using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// JSON serializable data representing the player data we want to save.
/// </summary>
[System.Serializable]
public class SaveData
{
    public bool TutorialDone;
    public bool SkipRollAnimations;
    public int HighScore;
    public int Rolls;
    public List<SaveAnimalData> AnimalData;

    public List<string> AnimalSkins;

    public SaveData(PlayerData data, List<SaveAnimalData> acquiredAnimalData)
    {
        TutorialDone = data.TutorialDone;
        SkipRollAnimations = data.SkipRollAnimations;
        HighScore = data.HighScore;
        Rolls = data.Rolls;
        AnimalData = acquiredAnimalData;
        AnimalSkins = data.currentAnimalSkins;
    }
}

/// <summary>
/// JSON serializable data representing data about an acquired animal
/// </summary>
[System.Serializable]
public class SaveAnimalData
{
    public string Id;
    public int Saved;

    public SaveAnimalData(string id, int saved)
    {
        Id = id;
        Saved = saved;
    }
}

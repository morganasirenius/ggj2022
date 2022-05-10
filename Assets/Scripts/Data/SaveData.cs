using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public bool TutorialDone;
    public int HighScore;
    public int Rolls;
    public List<SaveAnimalData> AnimalData;

    public List<Sprite> AnimalSkins;

    public SaveData(bool tutorialDone, int highScore, int rolls, List<SaveAnimalData> animalData, List<Sprite> animalSkins)
    {
        TutorialDone = tutorialDone;
        HighScore = highScore;
        Rolls = rolls;
        AnimalData = animalData;
        AnimalSkins = animalSkins;
    }
}

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

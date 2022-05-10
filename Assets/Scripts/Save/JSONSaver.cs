using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSaver : Singleton<JSONSaver>
{
    private string path;
    private string persistentPath;
    private string saveDataFileName = "SaveData.json";
    // Start is called before the first frame update
    void Start()
    {
        SetPaths();
    }

    public void SetPaths()
    {
        // path is used to debug save data since its saved to the project path
        // path = Application.dataPath + Path.AltDirectorySeparatorChar + saveDataFileName;
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + saveDataFileName;
        Debug.Log(persistentPath);
    }

    public void SaveData()
    {
        List<SaveAnimalData> acquiredAnimalData = new List<SaveAnimalData>();

        foreach (KeyValuePair<AnimalData, int> data in PlayerData.Instance.acquiredAnimals)
        {
            SaveAnimalData animalData = new SaveAnimalData(data.Key.animalName, data.Value);
            acquiredAnimalData.Add(animalData);
        }
        SaveData save = new SaveData(PlayerData.Instance.TutorialDone, PlayerData.Instance.HighScore, PlayerData.Instance.Rolls, acquiredAnimalData, PlayerData.Instance.currentAnimalSkins);
        string jsonData = JsonUtility.ToJson(save);
        using StreamWriter writer = new StreamWriter(persistentPath);
        writer.Write(jsonData);
    }

    public bool LoadData()
    {
        if (!File.Exists(persistentPath))
        {
            // File does not exist
            return false;
        }
        using StreamReader reader = new StreamReader(persistentPath);
        string jsonData = reader.ReadToEnd();
        SaveData save = JsonUtility.FromJson<SaveData>(jsonData);

        if (save != null)
        {
            Dictionary<AnimalData, int> savedAcquiredAnimals = new Dictionary<AnimalData, int>();
            // Load high score and rolls
            PlayerData.Instance.TutorialDone = save.TutorialDone;
            PlayerData.Instance.HighScore = save.HighScore;
            PlayerData.Instance.Rolls = save.Rolls;
            PlayerData.Instance.currentAnimalSkins = save.AnimalSkins;

            // Load acquired animals for collection
            foreach (SaveAnimalData saveData in save.AnimalData)
            {
                AnimalData data = ResourceManager.Instance.AnimalToDataDictionary[saveData.Id];
                savedAcquiredAnimals[data] = saveData.Saved;
            }
            PlayerData.Instance.acquiredAnimals = savedAcquiredAnimals;
            // File loaded
            return true;
        }
        // File retrieved but is emptied
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rename this to PlayerData
public class PlayerData : Singleton<PlayerData>
{
    public bool TutorialDone = false;
    public bool SkipRollAnimations = false;

    // Dictionary of animals acquired by rolling in gachapon
    public Dictionary<AnimalData, int> acquiredAnimals = new Dictionary<AnimalData, int>();
    // List of strings deonting the current animals used for the game
    public List<string> currentAnimalSkins = new List<string>();
    public int Rolls { get; set; }
    private int scoreUntilRoll = 0;
    public int HighScore = 0;
    private int m_PlayerScore = 0;
    public int PlayerScore
    {
        get { return m_PlayerScore; }
        set
        {
            if (m_PlayerScore == value) return;
            m_PlayerScore = value;
            if (m_PlayerScore > HighScore)
            {
                HighScore = PlayerScore;
                JSONSaver.Instance.SaveData();
            }
            if (OnScoreChange != null)
                OnScoreChange(m_PlayerScore, HighScore);
        }
    }
    public delegate void OnVariableChangeDelegate(int PlayerScore, int HighScore);
    public event OnVariableChangeDelegate OnScoreChange;
    public void AddScore(int score_num)
    {
        PlayerScore += score_num;
        scoreUntilRoll += score_num;
        if (scoreUntilRoll >= Globals.scoreForRoll)
        {
            scoreUntilRoll = 0;
            Rolls++;
            JSONSaver.Instance.SaveData();
        }
    }

    public void ResetScore()
    {
        PlayerScore = 0;
    }

    public void ResetHighScore()
    {
        HighScore = 0;
    }

    public void Start()
    {
        // Get saved values
        if (!JSONSaver.Instance.LoadData())
        {
            foreach (AnimalData animal in ResourceManager.Instance.DefaultAnimalArray)
            {
                currentAnimalSkins.Add(animal.animalName);
            }
        }
        Debug.Log(currentAnimalSkins.Count);
    }
}

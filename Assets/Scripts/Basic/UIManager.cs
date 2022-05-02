using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    //Game UI
    public TMP_Text ScoreText;
    public TMP_Text HighscoreText;
    public TMP_Text HealthText;
    public TMP_Text NukeChargeText;

    // End Game UI
    public GameObject EndScreenUI;
    public TMP_Text EndScreenHighscoreText;
    public TMP_Text EndScreenScoreText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData.Instance.OnScoreChange += UpdateText;
        UpdateText(0, PlayerData.Instance.HighScore);
    }

    public void UpdateHealth(int health)
    {
        HealthText.text = "Health: " + health;
    }
    public void UpdateText(int playScore, int highScore)
    {
        ScoreText.text = "Score: " + playScore.ToString();
        HighscoreText.text = "Highscore: " + highScore.ToString();
        EndScreenScoreText.text = "Your score: " + playScore.ToString();
        EndScreenHighscoreText.text = "Highscore: " + highScore.ToString();
    }

    public void UpdateNukeCharges(int charges)
    {
        NukeChargeText.text = "Charges: " + charges;
    }

    public void EndScreen()
    {
        EndScreenUI.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

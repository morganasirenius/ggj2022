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
    public TMP_Text BombChargeText;

    // End Game UI
    public GameObject EndScreenUI;
    public TMP_Text EndScreenHighscoreText;
    public TMP_Text EndScreenScoreText;

    // Mobile UI
    public GameObject MobileUI;

    // Pause UI
    public GameObject PauseUI;
    public GameObject PauseSettingsUI;

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

    public void UpdateBombCharges(int charges)
    {
        BombChargeText.text = "Bombs: " + charges;
    }

    public void EndScreen()
    {
        EndScreenUI.SetActive(true);
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelOne");
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        AudioManager.Instance.PlayMusic("Space Ambience");
        SceneManager.LoadScene("MainMenu");
    }

    public void DisplayMobileUI()
    {
        MobileUI.SetActive(true);
    }

    public void OpenPauseScreen()
    {
        PauseUI.SetActive(true);
    }

    public void ClosePauseScreen()
    {
        PauseUI.SetActive(false);
    }

    public void OpenPauseSettingsScreen()
    {
        PauseUI.SetActive(false);
        PauseSettingsUI.SetActive(true);
        // Prevents player from unpausing on accident
        PlayerController.Instance.inSettings = true;
    }

    public void ClosePauseSettingsScreen()
    {
        PauseSettingsUI.SetActive(false);
        PauseUI.SetActive(true);
        PlayerController.Instance.inSettings = false;
    }
}

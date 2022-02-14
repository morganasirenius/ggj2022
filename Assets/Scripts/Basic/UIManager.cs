using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    public TMP_Text ScoreText;
    public TMP_Text HighscoreText;
    public TMP_Text HealthText;
    private int score = 0;
    private int highscore = 0;
    // Start is called before the first frame update
    void Start()
    {
        updateText();
    }

    public void UpdateHealth(int health)
    {
        HealthText.text = "Health: " + health;
    }
    private void updateText()
    {
        ScoreText.text = "Score: " + score.ToString();
        HighscoreText.text = "Highscore: " + highscore.ToString();
    }
    public void AddScore(int score_num)
    {
        score += score_num;
        if (score > highscore) highscore = score;
        updateText();
    }

    public void ResetScore()
    {
        score = 0;
        updateText();
    }
}

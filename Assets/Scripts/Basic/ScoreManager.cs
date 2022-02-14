using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : Singleton<ScoreManager>
{
    public TMP_Text scoreText;
    public TMP_Text highscoreText;

    private int score = 0;
    private int highscore = 0;
    // Start is called before the first frame update
    void Start()
    {
        updateText();
    }

    private void updateText()
    {
        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "Highscore: " + highscore.ToString();
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

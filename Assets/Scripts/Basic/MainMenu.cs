using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MainScreen;
    [SerializeField]
    private GameObject CreditsScreen;
    public void StartGame()
    {

        if (PlayerData.Instance.TutorialDone)
        {
            SceneManager.LoadScene("LevelOne");
        }
        else
        {
            SceneManager.LoadScene("LevelTutorial");
        }
    }

    public void SetCreditsScreen()
    {
        MainScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void SetMainScreen()
    {
        CreditsScreen.SetActive(false);
        MainScreen.SetActive(true);
    }
}

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
    [SerializeField]
    private GameObject GachaScreen;
    [SerializeField]
    private GameObject GachaHelpScreen;
    [SerializeField]
    private GameObject CollectionScreen;
    [SerializeField]
    private GameObject CollectionHelpScreen;
    [SerializeField]
    private GameObject CurrentScreen;
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
        CurrentScreen.SetActive(false);
        CreditsScreen.SetActive(true);
        CurrentScreen = CreditsScreen;
    }

    public void SetMainScreen()
    {
        CurrentScreen.SetActive(false);
        MainScreen.SetActive(true);
        CurrentScreen = MainScreen;
    }
    public void SetGachaScreen()
    {
        CurrentScreen.SetActive(false);
        GachaScreen.SetActive(true);
        CurrentScreen = GachaScreen;
    }

    public void SetCollectionScreen()
    {
        CurrentScreen.SetActive(false);
        CollectionScreen.SetActive(true);
        CurrentScreen = CollectionScreen;
    }

    public void OpenGachaHelpScreen()
    {
        GachaHelpScreen.SetActive(true);
    }

    public void CloseGachaHelpScreen()
    {
        GachaHelpScreen.SetActive(false);
    }

    public void OpenCollectionHelpScreen()
    {
        CollectionHelpScreen.SetActive(true);
    }

    public void CloseCollectionHelpScreen()
    {
        CollectionHelpScreen.SetActive(false);
    }
}

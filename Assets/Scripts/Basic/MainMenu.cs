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
    private GameObject RollPurchaseScreen;
    [SerializeField]
    private GameObject CollectionScreen;
    [SerializeField]
    private GameObject CollectionHelpScreen;
    [SerializeField]
    private GameObject SettingsScreen;
    [SerializeField]
    private GameObject CurrentScreen;
    public void StartGame()
    {

        if (PlayerData.Instance.TutorialDone)
        {
            SceneManager.LoadScene("LevelOne");
            // AudioManager.Instance.PlayMusic("Pixel War 2");
        }
        else
        {
            SceneManager.LoadScene("LevelTutorial");
            AudioManager.Instance.PlayMusic("The Laboratory");
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

    public void SetSettingsScreen()
    {
        CurrentScreen.SetActive(false);
        SettingsScreen.SetActive(true);
        CurrentScreen = SettingsScreen;
    }

    public void OpenGachaHelpScreen()
    {
        GachaHelpScreen.SetActive(true);
    }

    public void CloseGachaHelpScreen()
    {
        GachaHelpScreen.SetActive(false);
    }

    public void OpenRollPurchaseScreen()
    {
        RollPurchaseScreen.SetActive(true);
        GachaScreen.SetActive(false);
    }

    public void CloseRollPurchaseScreen()
    {
        RollPurchaseScreen.SetActive(false);
        GachaScreen.SetActive(true);
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

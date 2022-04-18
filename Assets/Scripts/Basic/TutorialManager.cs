using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField]
    private GameObject tutorialEnemy;
    [SerializeField]
    private GameObject tutorialAlly;
    [SerializeField]
    private GameObject nukeEnemies;
    [SerializeField]
    private TMP_Text tutorialText;

    // State currentState;
    // State stateMovement;
    // State ...

    void Start()
    {
        // DisplayTutorialText(Globals.TutorialTextNames.Save);
        // Assign the first state here
    }
    void Update()
    {
        // Run current state actions here
    }

    // ChangeState changes states after some delay
    public void ChangeState(int delay = 0)
    {
        // TODO: Add parameter for state to change and implement
    }

    // DisplayTutorialText displays the relevant tutorial text
    public void DisplayTutorialText(Globals.TutorialTextNames textName)
    {
        tutorialText.text = ResourceManager.Instance.TutorialTextDictionary[textName.ToString()].text;
    }

    public void SpawnTutorialEnemy()
    {
        tutorialEnemy.SetActive(true);
    }
    public void SpawnTutorialAlly()
    {
        tutorialAlly.SetActive(true);
    }

    public void SpawnNukeEnemies()
    {
        nukeEnemies.SetActive(true);
    }
}

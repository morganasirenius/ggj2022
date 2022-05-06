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

    private State CurrentState;

    void Start()
    {
        // DisplayTutorialText(Globals.TutorialTextNames.Save);
        // Assign the first state here
        CurrentState = new TutorialIntroductionState();
        CurrentState.Enter();
    }
    void Update()
    {
        // Run current state actions here
        CurrentState.LogicUpdate();
    }

    IEnumerator NewState(State newState, int delay)
    {
        yield return new WaitForSeconds(delay);
        CurrentState = newState;
        newState.Enter();
    }
    // ChangeState changes states after some delay
    public void ChangeState(State newState, int delay = 1)
    {
        // TODO: Add parameter for state to change and implement
        CurrentState.Exit();
        IEnumerator coroutine = NewState(newState, delay);
        StartCoroutine(coroutine);
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

    public bool TutorialEnemyDead()
    {
        return !tutorialEnemy.activeSelf;
    }

    public void SpawnTutorialAlly()
    {
        tutorialAlly.SetActive(true);
    }

    public bool TutorialAllySave()
    {
        return !tutorialAlly.activeSelf;
    }

    public void SpawnNukeEnemies()
    {
        nukeEnemies.SetActive(true);
    }

    public bool TutorialNukeEnemiesDead()
    {
        foreach(Transform enemy in nukeEnemies.transform)
        {
            if (enemy.gameObject.activeSelf) return false;
        }
        return true;
    }
}

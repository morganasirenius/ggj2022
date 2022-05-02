using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialSwitchMainState : State
{
    public override void Enter()
    {
        base.Enter();
        PlayerData.Instance.TutorialDone = true;
        PlayerData.Instance.ResetHighScore();
        SceneManager.LoadScene("LevelOne");
        
    }

    public override void LogicUpdate()
    {
        
    }

    public override void Exit()
    {
    }
}

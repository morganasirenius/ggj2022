using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialExtraNukeState : State
{
    public override void Enter()
    {
        base.Enter();
        TutorialManager.Instance.DisplayTutorialText(Globals.TutorialTextNames.ExtraNuke);
        TutorialManager.Instance.ChangeState(new TutorialDoneState(), 6);
    }

    public override void LogicUpdate()
    {
       
    }

    public override void Exit()
    {
    }
}

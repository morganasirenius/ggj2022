using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialExtraBombState : State
{
    public override void Enter()
    {
        base.Enter();
        TutorialManager.Instance.DisplayTutorialText(Globals.TutorialTextNames.ExtraBomb);
        TutorialManager.Instance.ChangeState(new TutorialDoneState(), 8);
    }

    public override void LogicUpdate()
    {
       
    }

    public override void Exit()
    {
    }
}

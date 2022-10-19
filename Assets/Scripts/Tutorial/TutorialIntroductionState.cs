using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialIntroductionState : State
{
    public override void Enter()
    {
        base.Enter();
        TutorialManager.Instance.DisplayTutorialText(Globals.TutorialTextNames.Introduction);
        TutorialManager.Instance.ChangeState(new TutorialMovementState(), 10);
    }

    public override void LogicUpdate()
    {
       
    }

    public override void Exit()
    {
    }
}

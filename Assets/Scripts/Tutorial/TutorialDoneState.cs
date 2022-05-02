using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoneState : State
{
    public override void Enter()
    {
        base.Enter();
        //Display Dialogue UI
        TutorialManager.Instance.DisplayTutorialText(Globals.TutorialTextNames.Done);
        TutorialManager.Instance.ChangeState(new TutorialSwitchMainState(), 4);
    }

    public override void LogicUpdate()
    {
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}

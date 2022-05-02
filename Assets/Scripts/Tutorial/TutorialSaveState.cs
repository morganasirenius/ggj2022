using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSaveState : State
{
    public override void Enter()
    {
        base.Enter();
        //Display Dialogue UI
        TutorialManager.Instance.DisplayTutorialText(Globals.TutorialTextNames.Save);
        //Spawn enemy
        TutorialManager.Instance.SpawnTutorialAlly();
    }

    public override void LogicUpdate()
    {
        if (TutorialManager.Instance.TutorialAllySave())
        {
            TutorialManager.Instance.ChangeState(new TutorialExtraNukeState());

        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

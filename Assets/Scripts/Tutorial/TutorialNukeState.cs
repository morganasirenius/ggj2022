using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNukeState : State
{
    public override void Enter()
    {
        base.Enter();
        //Display Dialogue UI
        TutorialManager.Instance.DisplayTutorialText(Globals.TutorialTextNames.Nuke);
        //Spawn enemy
        TutorialManager.Instance.SpawnNukeEnemies();
    }

    public override void LogicUpdate()
    {
        if (TutorialManager.Instance.TutorialNukeEnemiesDead())
        {
            TutorialManager.Instance.ChangeState(new TutorialSaveState());
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

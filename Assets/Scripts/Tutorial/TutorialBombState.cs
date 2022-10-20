using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBombState : State
{
    public override void Enter()
    {
        base.Enter();
        //Display Dialogue UI
        TutorialManager.Instance.DisplayTutorialText(Globals.TutorialTextNames.Bomb);
        //Spawn enemy
        TutorialManager.Instance.SpawnBombEnemies();
    }

    public override void LogicUpdate()
    {
        if (TutorialManager.Instance.TutorialBombEnemiesDead())
        {
            TutorialManager.Instance.ChangeState(new TutorialSaveState());
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

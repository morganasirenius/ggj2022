using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TutorialAttackState : State
{

    public override void Enter()
    {
        base.Enter();
        //Display Dialogue UI
        TutorialManager.Instance.DisplayTutorialText(Globals.TutorialTextNames.Attack);
        //Spawn enemy
        TutorialManager.Instance.SpawnTutorialEnemy();
    }

    public override void LogicUpdate()
    {
        if (TutorialManager.Instance.TutorialEnemyDead())
        {
            TutorialManager.Instance.ChangeState(new TutorialBombState());
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

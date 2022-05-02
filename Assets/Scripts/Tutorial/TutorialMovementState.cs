using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TutorialMovementState : State
{
    private bool left_triggered = false;
    private bool right_triggered = false;
    private bool up_triggered = false;
    private bool down_triggered = false;

    public override void Enter()
    {
        base.Enter();
        playerControls.Space.Move.performed += HandleInput;
        //Display Dialogue UI
        TutorialManager.Instance.DisplayTutorialText(Globals.TutorialTextNames.Movement);
    }

    public override void HandleInput(InputAction.CallbackContext context)
    {
        base.HandleInput(context);
        Vector2 movement = playerControls.Space.Move.ReadValue<Vector2>();
        if (movement.x < 0)
        {
            left_triggered = true;
        }
        else if (movement.x > 0)
        {
            right_triggered = true;
        }
        if (movement.y < 0)
        {
            down_triggered = true;
        }
        else if (movement.y > 0)
        {
            up_triggered = true;

        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (left_triggered && right_triggered && up_triggered && down_triggered)
        {
            TutorialManager.Instance.ChangeState(new TutorialAttackState());
        }
    }

    public override void Exit()
    {
        base.Exit();
        playerControls.Space.Move.performed -= HandleInput;
    }
}

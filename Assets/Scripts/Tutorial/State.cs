using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class State
{
    public PlayerControls playerControls;

    public State()
    {
        playerControls = PlayerController.Instance.playerControls;
    }

    public virtual void Enter()
    {
    }

    public virtual void HandleInput(InputAction.CallbackContext context)
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void Exit()
    {

    }

}

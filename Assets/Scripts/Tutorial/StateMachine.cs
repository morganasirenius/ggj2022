using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public State CurrentState;
    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }

}

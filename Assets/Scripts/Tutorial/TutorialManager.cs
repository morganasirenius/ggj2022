using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    StateMachine tutorialSM;
    int currentState = 0;
    List<State> tutorialStates;
    // Start is called before the first frame update
    void Start()
    {
        tutorialSM = new StateMachine();

        tutorialStates = new List<State>()
        {
            new TutorialMovementState()
        };

        tutorialSM.Initialize(tutorialStates[0]);

    }

    // Update is called once per frame
    void Update()
    {
        tutorialSM.CurrentState.LogicUpdate();
    }
}

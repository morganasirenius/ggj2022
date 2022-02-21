using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    [Tooltip("The direction the enemy moves to.")]
    [SerializeField]
    private Globals.Direction moveDirection;

    [Tooltip("How long the enemy moves for until it needs to stop. Needs Stop Duration to work.")]
    [SerializeField]
    private float moveDuration;
    [Tooltip("How long the enemy stops for before moving forward. Needs Move Duration to work.")]
    [SerializeField]
    private float stopDuration;

    [Tooltip("Decides if the enemy should fire only when it stops or during continous movement.")]
    [SerializeField]
    private bool fireWhenStopped;

    enum MovementStates
    {
        Moving = 0,
        Stopped
    }

    private Vector3 direction;
    private bool shouldStop;
    private float currentMoveTime, currentStopTime;
    private MovementStates state;
    void Start()
    {
        Initialize();
    }

    void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        switch (moveDirection)
        {
            case Globals.Direction.Up:
                direction = Vector3.up;
                break;
            case Globals.Direction.Down:
                direction = Vector3.down;
                break;
            case Globals.Direction.Left:
                direction = Vector3.left;
                break;
            case Globals.Direction.Right:
                direction = Vector3.right;
                break;
            default:
                break;
        }

        if (moveDuration > 0)
        {
            shouldStop = true;
            currentMoveTime = moveDuration;
            currentStopTime = stopDuration;
            state = MovementStates.Moving;
        }
    }
    public override void Move()
    {
        base.Move();
        if (ShouldMove())
        {
            this.transform.position += direction * speed * Time.deltaTime;
        }
    }

    public bool ShouldMove()
    {
        // Check if enemy should be stopping first
        if (shouldStop)
        {
            // Decrement counters for moving/stopping depending on the state
            switch (state)
            {
                case MovementStates.Moving:
                    currentMoveTime -= Time.deltaTime;
                    if (currentMoveTime < 0)
                    {
                        state = MovementStates.Stopped;
                        return false;
                    }
                    return true;
                case MovementStates.Stopped:
                    currentStopTime -= Time.deltaTime;
                    if (stopDuration != 0 && currentStopTime < 0)
                    {
                        shouldStop = false;
                        return true;
                    }
                    return false;
                default:
                    break;
            }
        }
        return true;
    }

    public override void Attack()
    {
        // Fire only if the enemy should stop/fire when stopped, and its moving
        if (shouldStop && fireWhenStopped && state == MovementStates.Moving)
        {
            return;
        }
        projectileModule.Fire(direction);
    }
}

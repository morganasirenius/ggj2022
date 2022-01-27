using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public enum Action
    {
        Spawn = 0,
        Delay
    }

    public enum EntityType
    {
        Ally = 0,
        AimDownEnemy,
        AimLeftEnemy,
        AimRightEnemy,
        BurstDownEnemy,
        CurveLeftEnemy,
        CurveRightEnemy,
        SpreadDownEnemy,
        StraightDownEnemy
    }

    public enum Direction
    {
        Up = 0,
        Down,
        Left,
        Right
    }
    public enum SpawnPoints
    {
        Start = 0,
        Middle,
        End,
        Random
    }

    // Determines how entities should be spawned
    public enum SpawnStyle
    {

        Same = 0,
        Random
    }
}

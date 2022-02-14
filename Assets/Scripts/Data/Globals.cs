using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
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

    public struct EntityProperties
    {
        public int maxSpawnCount;
        public Direction spawnDirection;

        public List<SpawnPoints> possibleSpawnPoints;
        public List<SpawnStyle> possibleSpawnStyles;
    }


    public static Dictionary<EntityType, EntityProperties> entityMap = new Dictionary<EntityType, EntityProperties>()
    {
        // Allies
        {(EntityType.Ally), new EntityProperties
                                        {
                                            maxSpawnCount = 3,
                                            spawnDirection = Direction.Up,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Random}
                                        }},
        // Aiming Enemies
        {(EntityType.AimDownEnemy), new EntityProperties
                                        {
                                            maxSpawnCount = 5,
                                            spawnDirection = Direction.Up,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same, SpawnStyle.Random}
                                        }},
        {(EntityType.AimLeftEnemy), new EntityProperties
                                        {
                                            maxSpawnCount = 5,
                                            spawnDirection = Direction.Right,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same, SpawnStyle.Random}
                                        }},
        {(EntityType.AimRightEnemy), new EntityProperties
                                        {
                                            maxSpawnCount = 5,
                                            spawnDirection = Direction.Left,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same, SpawnStyle.Random}
                                        }},
        // Burst Enemies
        {(EntityType.BurstDownEnemy), new EntityProperties
                                        {
                                            maxSpawnCount = 3,
                                            spawnDirection = Direction.Up,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same, SpawnStyle.Random}
                                        }},
        // Curve Enemies
        {(EntityType.CurveLeftEnemy), new EntityProperties
                                        {
                                            maxSpawnCount = 5,
                                            spawnDirection = Direction.Right,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same}
                                        }},
        {(EntityType.CurveRightEnemy), new EntityProperties
                                        {
                                            spawnDirection = Direction.Left,
                                            maxSpawnCount = 5,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same}
                                        }},
        // Spread Enemies
        {(EntityType.SpreadDownEnemy), new EntityProperties
                                        {
                                            spawnDirection = Direction.Up,
                                            maxSpawnCount = 5,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same, SpawnStyle.Random}
                                        }},
        // Straight Down Enemies
        {(EntityType.StraightDownEnemy), new EntityProperties
                                        {
                                            spawnDirection = Direction.Up,
                                            maxSpawnCount = 5,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same, SpawnStyle.Random}
                                        }},
    };
}

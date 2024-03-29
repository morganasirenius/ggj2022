using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public const string BackgroundVolumeKey = "backgroundVolume";
    public const string SFXVolumeKey = "sfxVolume";
    public const string BeamVolumeKey = "beamVolume";
    // For every X score, get a roll
    public static int scoreForRoll = 500;
    public enum GachaponRarities
    {
        Nice = 0,
        Great,
        Awesome,
        Wonderful,
        Amazing,
    }

    public static Dictionary<GachaponRarities, int> gachaponProbabilities = new Dictionary<GachaponRarities, int>()
    {
        {(GachaponRarities.Nice), 59},
        {(GachaponRarities.Great), 25},
        {(GachaponRarities.Awesome), 10},
        {(GachaponRarities.Wonderful), 5},
        {(GachaponRarities.Amazing), 1},
    };
    public static Dictionary<GachaponRarities, Color> rarityToColor = new Dictionary<GachaponRarities, Color>()
    {
        {(GachaponRarities.Nice), new Color32(30,32,36,255)}, // Gray
        {(GachaponRarities.Great), new Color32(9,82,24,255)}, // Green
        {(GachaponRarities.Awesome), new Color32(14,50,73,255)}, // Blue
        {(GachaponRarities.Wonderful), new Color32(56,14,81,255)}, // Purple
        {(GachaponRarities.Amazing), new Color32(90,86,35,255)}, // Golden
    };

    public static Dictionary<int, int> rollsToPoints = new Dictionary<int, int>()
    {
        {(1), 1000},
        {(10), 8000},
        {(25), 20000},
        {(50), 40000},
        {(100), 80000},
    };

    public static List<RollProperty> rollProperties = new List<RollProperty>()
    {
        {
             new RollProperty {
                rolls = 1,
                price = 1000
             }
        },
        {
             new RollProperty {
                rolls = 10,
                price = 8000
             }
        },
        {
             new RollProperty {
                rolls = 25,
                price = 20000
             }
        },
        {
             new RollProperty {
                rolls = 50,
                price = 40000
             }
        },
        {
             new RollProperty {
                rolls = 100,
                price = 80000
             }
        },
    };

    public struct RollProperty
    {
        public int rolls;
        public int price;
    }

    // Defines tutorial text associated with a part of a tutorial
    // These enums map 1-1 with the associated scriptable object
    public enum TutorialTextNames
    {
        Movement = 0,
        Attack,
        Bomb,
        Save,
        Done,
        Introduction,
        ExtraBomb
    }
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
        public int spawnRate;
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
                                            spawnRate = 20,
                                            maxSpawnCount = 3,
                                            spawnDirection = Direction.Up,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Random}
                                        }},
        // Aiming Enemies
        {(EntityType.AimDownEnemy), new EntityProperties
                                        {
                                            spawnRate = 10,
                                            maxSpawnCount = 5,
                                            spawnDirection = Direction.Up,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Random}
                                        }},
        {(EntityType.AimLeftEnemy), new EntityProperties
                                        {
                                            spawnRate = 10,
                                            maxSpawnCount = 5,
                                            spawnDirection = Direction.Right,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same, SpawnStyle.Random}
                                        }},
        {(EntityType.AimRightEnemy), new EntityProperties
                                        {
                                            spawnRate = 10,
                                            maxSpawnCount = 5,
                                            spawnDirection = Direction.Left,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same, SpawnStyle.Random}
                                        }},
        // Burst Enemies
        {(EntityType.BurstDownEnemy), new EntityProperties
                                        {
                                            spawnRate = 10,
                                            maxSpawnCount = 3,
                                            spawnDirection = Direction.Up,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Random}
                                        }},
        // Curve Enemies
        {(EntityType.CurveLeftEnemy), new EntityProperties
                                        {
                                            spawnRate = 10,
                                            maxSpawnCount = 5,
                                            spawnDirection = Direction.Right,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same}
                                        }},
        {(EntityType.CurveRightEnemy), new EntityProperties
                                        {
                                            spawnRate = 10,
                                            spawnDirection = Direction.Left,
                                            maxSpawnCount = 5,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same}
                                        }},
        // Spread Enemies
        {(EntityType.SpreadDownEnemy), new EntityProperties
                                        {
                                            spawnRate = 10,
                                            spawnDirection = Direction.Up,
                                            maxSpawnCount = 5,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same, SpawnStyle.Random}
                                        }},
        // Straight Down Enemies
        {(EntityType.StraightDownEnemy), new EntityProperties
                                        {
                                            spawnRate = 10,
                                            spawnDirection = Direction.Up,
                                            maxSpawnCount = 5,
                                            possibleSpawnPoints = new List<SpawnPoints>(){SpawnPoints.Start, SpawnPoints.Middle, SpawnPoints.Random},
                                            possibleSpawnStyles = new List<SpawnStyle>(){SpawnStyle.Same, SpawnStyle.Random}
                                        }},
    };
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A level data contains information about a level.
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelData : ScriptableObject
{
    // The name of the level
    public string levelName;
    public List<Globals.EntityType> levelEntities;
    // The commands used for the level. Each command is a string separated by semicolons
    public List<String> levelCommands;
}

/*
Current Commands
SPAWN - Spawns an entity at a position
DELAY - delays the next command

Parameters
SPAWN;TYPE;SPAWNER_LOCATION;SPAWN_POSITION;SPAWN_STYLE;COUNT;DELAY
TYPE - The entity type to spawn
SPAWNER_LOCATION - The location of which the entity spawns in. See Globals.Direction for possible values.
SPAWN_POSITION - Where the entity spawns in from the spawner. See Globals.Spawnpoints for possible values.
SPAWN_STYLE - How the entities will spawn. See Globals.SpawnStyles for possible values. If same, all entities will spawn in the same location. If random, each entity will have their own random spawnpoint.
COUNT - How many entities to spawn.
Delay - The delay between spawning each entity. 

DELAY;TIME
TIME - How long to delay for.
*/

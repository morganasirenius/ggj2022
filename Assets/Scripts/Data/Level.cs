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
SPAWN - spawns an entity at a positiona
DELAY - delays the next command

Parameters
SPAWN;TYPE;COUNT;POSITION;DELAY
DELAY;TIME
*/

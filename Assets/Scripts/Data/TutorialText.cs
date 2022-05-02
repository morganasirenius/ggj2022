using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A level data contains information about a level.
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TutorialText", order = 1)]
public class TutorialText : ScriptableObject
{
    [SerializeField, TextArea]
    public string text;
}


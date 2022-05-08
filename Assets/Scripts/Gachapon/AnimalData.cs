using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An animal data contains information about an animal.
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AnimalData", order = 1)]
public class AnimalData : ScriptableObject
{
    // The name of the animal
    public string animalName;
    // The actual sprite associated with the animal
    public Sprite sprite;
    // The rarity of the animal
    public Globals.GachaponRarities rarity;
}
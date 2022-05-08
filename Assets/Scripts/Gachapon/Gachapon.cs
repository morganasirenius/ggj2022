using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gachapon : MonoBehaviour
{
    [SerializeField]
    private Image GachaImage;

    [SerializeField]
    private TMP_Text GachaText;

    public void Roll()
    {
        Globals.GachaponRarities rarity = DetermineRarity();
        AnimalData[] dataArray = ResourceManager.Instance.AnimalDataDictionary[rarity.ToString()];
        AnimalData animal = dataArray[Random.Range(0, dataArray.Length)];
        GachaImage.sprite = animal.sprite;
        if (PlayerData.Instance.acquiredAnimals.ContainsKey(animal))
        {
            PlayerData.Instance.acquiredAnimals[animal] += 1;
        }
        else
        {
            PlayerData.Instance.acquiredAnimals[animal] = 1;
        }
        GachaText.text = string.Format("You got a {0} animal: {1}!", animal.rarity.ToString(), animal.animalName);
    }

    public Globals.GachaponRarities DetermineRarity()
    {
        int randomProb = Random.Range(0, 101); // Random.Range for ints takes an exclusive upper bound
        Debug.Log(string.Format("Probability: {0}!", randomProb));
        int cumulativeProb = 0;
        foreach (Globals.GachaponRarities rarity in System.Enum.GetValues(typeof(Globals.GachaponRarities)))
        {
            cumulativeProb += Globals.gachaponProbabilities[rarity];
            if (randomProb <= cumulativeProb)
            {
                return rarity;
            }
        }
        return Globals.GachaponRarities.Common;
    }
}
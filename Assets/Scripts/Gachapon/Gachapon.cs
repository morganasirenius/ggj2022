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

    [SerializeField]
    private TMP_Text RollsText;

    void OnEnable()
    {
        GachaText.text = "Roll to see what you get!";
        RollsText.text = PlayerData.Instance.Rolls.ToString();
        // Set sprite to be invisible
        GachaImage.color = new Color32(255, 255, 255, 0);
        Debug.Log(string.Format("You have {0} rolls!", PlayerData.Instance.Rolls.ToString()));
    }

    public void Roll()
    {
        if (PlayerData.Instance.Rolls > 0)
        {
            // Set sprite to be visible if it was previously invisible
            if (GachaImage.color.a == 0)
            {
                GachaImage.color = new Color32(255, 255, 255, 255);
            }

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
            GachaText.text = string.Format("{0}! \n You got {1}!", animal.rarity.ToString(), animal.animalName);

            PlayerData.Instance.Rolls--;
            RollsText.text = PlayerData.Instance.Rolls.ToString();
            JSONSaver.Instance.SaveData();
            Debug.Log(string.Format("Rolls remaining: {0}", PlayerData.Instance.Rolls.ToString()));
        }
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
        return Globals.GachaponRarities.Nice;
    }
}
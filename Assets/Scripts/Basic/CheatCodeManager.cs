using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class CheatCodeManager : Singleton<CheatCodeManager>
{
    private static readonly List<string> unlockAllAnimalsOrder = new List<string>{"Cat", "Llama", "Unicorn", "Kangaroo", "Turtle"};
    private List<string> userOrder = new List<string>{};
    private int starClickCount = 0;
    [SerializeField]
    private int numClicksForRoll = 10;
    [SerializeField]
    private int numRolls = 100;
    [SerializeField]
    private TMP_Text rollsText;
    
    public void AddAnimalOrder(string animalName)
    {
        userOrder.Add(animalName);
        if (userOrder.Count == 5)
        {
            if (Enumerable.SequenceEqual(userOrder, unlockAllAnimalsOrder))
            {
                AudioManager.Instance.PlaySfx("ohyesdesuwa", 2f);
            }
            else
            {
                AudioManager.Instance.PlaySfx("hit-2", 0.2f);
            }
            userOrder.Clear();
        }
    }

    public void AddRolls()
    {
        starClickCount++;
        if (starClickCount >= numClicksForRoll)
        {
            starClickCount = 0;
            PlayerData.Instance.Rolls += numRolls;
            rollsText.text = PlayerData.Instance.Rolls.ToString();
            JSONSaver.Instance.SaveData();
            AudioManager.Instance.PlaySfx("ohyesdesuwa", 2f);
        }
    }
}

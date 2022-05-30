using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gachapon : MonoBehaviour
{
    [SerializeField]
    private GachaponItem gachaItem;

    [SerializeField]
    private TMP_Text rarityText;

    [SerializeField]
    private TMP_Text animalText;

    [SerializeField]
    private TMP_Text rollsText;

    [SerializeField]
    private ParticleSystem beamUp;
    [SerializeField]
    private ParticleSystem beamDown;
    [SerializeField]
    private ParticleSystem baseCircle;

    private bool rolling;

    void OnEnable()
    {
        rarityText.text = "Gachapon";
        animalText.text = "Roll to see what you get!";
        rollsText.text = PlayerData.Instance.Rolls.ToString();
        gachaItem.InitializeImage();
        Debug.Log(string.Format("You have {0} rolls!", PlayerData.Instance.Rolls.ToString()));
    }

    public void Roll()
    {
        if (PlayerData.Instance.Rolls <= 0 && !rolling)
        {

            Globals.GachaponRarities rarity = DetermineRarity();
            AnimalData[] dataArray = ResourceManager.Instance.AnimalDataDictionary[rarity.ToString()];
            AnimalData animal = dataArray[Random.Range(0, dataArray.Length)];
            StartCoroutine(GachaAnimation(rarity, animal));

            if (PlayerData.Instance.acquiredAnimals.ContainsKey(animal))
            {
                PlayerData.Instance.acquiredAnimals[animal] += 1;
            }
            else
            {
                PlayerData.Instance.acquiredAnimals[animal] = 1;
            }


            PlayerData.Instance.Rolls--;
            rollsText.text = PlayerData.Instance.Rolls.ToString();
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

    IEnumerator GachaAnimation(Globals.GachaponRarities rarity, AnimalData animal)
    {
        rolling = true;
        // Set the beam color based off of rarity
        Color color = Globals.rarityToColor[rarity];
        ParticleSystem.TrailModule trailBeamUp = beamUp.trails;
        trailBeamUp.colorOverTrail = color;

        ParticleSystem.TrailModule trailBeamDown = beamDown.trails;
        trailBeamDown.colorOverTrail = color;

        // Start playing the particle effects
        baseCircle.Play();
        //AudioManager.Instance.PlaySfx("beam_circle_spin", 0.1f);
        yield return new WaitForSeconds(1.3f);
        beamUp.Play();
        yield return new WaitForSeconds(1.7f);
        beamDown.Play();
        //AudioManager.Instance.PlaySfx("beam_down", 0.1f);
        yield return new WaitForSeconds(0.8f);
        gachaItem.SetSprite(animal.sprite);
        rarityText.text = animal.rarity.ToString();
        animalText.text = animal.animalName;
        rolling = false;
    }
}
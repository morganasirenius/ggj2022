using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private TMP_Text skipAnimationTexts;

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
        SetSkipAnimationText();
        gachaItem.InitializeImage();
        Debug.Log(string.Format("You have {0} rolls!", PlayerData.Instance.Rolls.ToString()));
    }

    public void Roll()
    {
        if (PlayerData.Instance.Rolls > 0 && !rolling)
        {
            Globals.GachaponRarities rarity = DetermineRarity();
            AnimalData[] dataArray = ResourceManager.Instance.RarityToAnimalDataDictionary[rarity.ToString()];
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
        foreach (Globals.GachaponRarities
            rarity
            in
            System.Enum.GetValues(typeof(Globals.GachaponRarities))
        )
        {
            cumulativeProb += Globals.gachaponProbabilities[rarity];
            if (randomProb <= cumulativeProb)
            {
                return rarity;
            }
        }
        return Globals.GachaponRarities.Nice;
    }

    // Note: Most WaitforSeconds are delays based off the particle effects to simulate the whole beam animation
    IEnumerator GachaAnimation(Globals.GachaponRarities rarity, AnimalData animal)
    {
        rolling = true;
        if (!PlayerData.Instance.SkipRollAnimations)
        {
            // Set the beam color based off of rarity
            Color color = Globals.rarityToColor[rarity];
            ParticleSystem.TrailModule trailBeamUp = beamUp.trails;
            trailBeamUp.colorOverTrail = color;

            ParticleSystem.TrailModule trailBeamDown = beamDown.trails;
            trailBeamDown.colorOverTrail = color;

            // Start playing the particle effects
            baseCircle.Play();
            AudioManager.Instance.PlaySfx("beam_circle_spin");
            yield return new WaitForSeconds(1.3f);
            beamUp.Play();
            yield return new WaitForSeconds(1.7f);
            beamDown.Play();
            AudioManager.Instance.PlaySfx("beam_down");
            yield return new WaitForSeconds(0.8f);
        }
        gachaItem.SetSprite(animal.sprite);
        rarityText.text = animal.rarity.ToString();
        animalText.text = animal.animalName;
        // Add delay before clicking on the roll button if animations are not disabled
        if (!PlayerData.Instance.SkipRollAnimations)
        {
            yield return new WaitForSeconds(4.0f);

        }
        rolling = false;
    }

    public void ToggleSkipAnimation()
    {
        Debug.Log("Toggling Skip Animation!");
        PlayerData.Instance.SkipRollAnimations = !PlayerData.Instance.SkipRollAnimations;
        JSONSaver.Instance.SaveData();
        SetSkipAnimationText();
    }

    public void SetSkipAnimationText()
    {
        if (PlayerData.Instance.SkipRollAnimations)
        {
            skipAnimationTexts.text = "Skip Animation: On";
        }
        else
        {
            skipAnimationTexts.text = "Skip Animation: Off";
        }
    }
}

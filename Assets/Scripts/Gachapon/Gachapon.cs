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
        AnimalData animal = ResourceManager.Instance.AnimalDataArray[Random.Range(0, ResourceManager.Instance.AnimalDataArray.Length)];
        GachaImage.sprite = animal.sprite;

        GachaText.text = string.Format("You got {0}!", animal.animalName);
    }
}

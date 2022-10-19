using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public class RollPurchase : MonoBehaviour
{
    [SerializeField]
    private TMP_Text outerRollsText;
    [SerializeField]
    private TMP_Text innerRollsText;
    [SerializeField]
    private TMP_Text pointsText;

    [SerializeField]
    private List<GameObject> rollSlots;

    private int rollIndex;
    private Material currentSlot;


    void OnEnable()
    {
        UpdateCurrencies();
        rollIndex = -1;
        if (currentSlot != null)
        {
            currentSlot.DisableKeyword("ALPHAOUTLINE_ON");
            currentSlot = null;
        }
    }

    void Start()
    {
        // Disable all the outline shaders at the start
        // We keep them on by default since the package we use does not have a great way of 
        // actually enabling them in builds
        foreach (GameObject slot in rollSlots)
        {
            Material slotMaterial = slot.GetComponent<Image>().material;
            slotMaterial.DisableKeyword("ALPHAOUTLINE_ON");
        }
    }

    public void UpdateCurrencies()
    {
        outerRollsText.text = PlayerData.Instance.Rolls.ToString();
        innerRollsText.text = PlayerData.Instance.Rolls.ToString();
        pointsText.text = PlayerData.Instance.TotalScore.ToString();
    }

    public void PurchaseRolls()
    {
        // If no selection is made, return
        if (rollIndex == -1)
        {
            return;
        }
        Globals.RollProperty rollProperty = Globals.rollProperties[rollIndex];
        int score = PlayerData.Instance.TotalScore;
        if (rollProperty.price > score)
        {
            return;
        }
        PlayerData.Instance.UpdateScore(-rollProperty.price);
        PlayerData.Instance.Rolls += rollProperty.rolls;
        UpdateCurrencies();
        AudioManager.Instance.PlaySfx("collect-5", 0.5f);
    }

    public void SelectRollOption(int index)
    {
        if (currentSlot != null)
        {
            currentSlot.DisableKeyword("ALPHAOUTLINE_ON");
        }
        Material slotMaterial = rollSlots[index].GetComponent<Image>().material;
        currentSlot = slotMaterial;
        currentSlot.EnableKeyword("ALPHAOUTLINE_ON");

        rollIndex = index;

        Debug.Log(string.Format("Rolls to purchase set to {0}", rollIndex));
    }
}

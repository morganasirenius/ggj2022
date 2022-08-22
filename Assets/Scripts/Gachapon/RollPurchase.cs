using UnityEngine;
using TMPro;


public class RollPurchase : MonoBehaviour
{
    [SerializeField]
    private TMP_Text outerRollsText;
    [SerializeField]
    private TMP_Text innerRollsText;
    [SerializeField]
    private TMP_Text pointsText;

    void OnEnable()
    {
        UpdateCurrencies();
    }

    public void UpdateCurrencies()
    {
        outerRollsText.text = PlayerData.Instance.Rolls.ToString();
        innerRollsText.text = "Rolls: " + PlayerData.Instance.Rolls;
        pointsText.text = "Score: " + PlayerData.Instance.TotalScore;
    }

    public void PurchaseRolls(int rolls)
    {
        int price = Globals.rollsToPoints[rolls];
        int score = PlayerData.Instance.TotalScore;
        if (price > score)
        {
            return;
        }
        PlayerData.Instance.UpdateScore(-price);
        PlayerData.Instance.Rolls += rolls;
        UpdateCurrencies();
    }
}

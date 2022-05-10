using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AnimalCollection : MonoBehaviour
{
    [SerializeField]
    private GameObject CollectionGrid;
    [SerializeField]
    private GameObject SelectedGrid;
    [SerializeField]
    private Image CurrentAnimalImage;
    [SerializeField]
    private TMP_Text CurrentAnimalText;
    [SerializeField]
    private TMP_Text AnimalCountText;

    private List<GameObject> selectionSlots;
    private GameObject currentSelectedAnimal;
    private Sprite selectedSprite;
    private int selectionIndex;

    // Start is called before the first frame update
    void Start()
    {
        SetupCurrentAnimals();
    }

    void OnEnable()
    {
        CurrentAnimalText.text = "Select an animal!";
        AnimalCountText.text = "";
        // Set sprite to be invisible
        CurrentAnimalImage.color = new Color32(255, 255, 255, 0);
        SetupCollection();
    }

    void OnDisable()
    {
        CleanUpCollection();
    }

    public void SetupCurrentAnimals()
    {
        selectionSlots = new List<GameObject>();
        for (int i = 0; i < PlayerData.Instance.currentAnimalSkins.Count; i++)
        {
            // Need to define a local variable since i dynamically changes for listeners
            int slotIndex = i;
            Sprite sprite = PlayerData.Instance.currentAnimalSkins[i];
            GameObject slot = (GameObject)Instantiate(Resources.Load("Prefabs/Gachapon/Slot"));
            slot.transform.SetParent(SelectedGrid.transform, false);
            GameObject cardGameObject = slot.transform.Find("Card").gameObject;
            GameObject animalGameObject = slot.transform.Find("Animal").gameObject;
            animalGameObject.GetComponent<Image>().sprite = sprite;
            cardGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("CollectionView/SelectedAnimal");
            slot.GetComponent<Button>().onClick.AddListener(delegate
            {
                selectionIndex = slotIndex;
                // Highlight the clicked animal
                animalGameObject.GetComponent<Image>().color = Color.green;
                // If an animal is already selected, unhighlight it
                if (currentSelectedAnimal != null && currentSelectedAnimal != animalGameObject)
                {
                    currentSelectedAnimal.GetComponent<Image>().color = Color.white;
                }
                currentSelectedAnimal = animalGameObject;
            });
            selectionSlots.Add(slot);
        }
    }

    public void SetupCollection()
    {
        foreach (KeyValuePair<AnimalData, int> data in PlayerData.Instance.acquiredAnimals)
        {
            GameObject slot = (GameObject)Instantiate(Resources.Load("Prefabs/Gachapon/Slot"));
            slot.transform.SetParent(CollectionGrid.transform, false);
            GameObject cardGameObject = slot.transform.Find("Card").gameObject;
            GameObject animalGameObject = slot.transform.Find("Animal").gameObject;
            animalGameObject.GetComponent<Image>().sprite = data.Key.sprite;
            cardGameObject.GetComponent<Image>().sprite = ResourceManager.Instance.RarityCardsDictionary[data.Key.rarity.ToString()];
            slot.GetComponent<Button>().onClick.AddListener(delegate
            {
                // Set sprite to be visible if it was previously invisible
                Debug.Log(CurrentAnimalImage.color.a);
                if (CurrentAnimalImage.color.a == 0)
                {
                    CurrentAnimalImage.color = new Color32(255, 255, 255, 255);
                }

                CurrentAnimalText.text = data.Key.animalName;
                CurrentAnimalImage.sprite = data.Key.sprite;
                AnimalCountText.text = "Saved: " + data.Value.ToString();
            });
        }
    }

    public void SetSelectedAnimal()
    {
        PlayerData.Instance.currentAnimalSkins[selectionIndex] = CurrentAnimalImage.sprite;
        selectionSlots[selectionIndex].transform.Find("Animal").GetComponent<Image>().sprite = CurrentAnimalImage.sprite;
        JSONSaver.Instance.SaveData();
    }

    public void CleanUpCollection()
    {
        foreach (Transform child in CollectionGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

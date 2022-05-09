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

    private List<GameObject> SelectionSlots;
    private Sprite SelectedSprite;
    private int SelectionIndex;

    // Start is called before the first frame update
    void Start()
    {
        //SetupCollection();
        SetupCurrentAnimals();
    }

    void OnEnable()
    {
        SetupCollection();
        // SetupCurrentAnimals();
    }

    void OnDisable()
    {
        CleanUpCollection();
    }

    public void SetupCurrentAnimals()
    {
        SelectionSlots = new List<GameObject>();

        for (int i = 0; i < PlayerData.Instance.currentAnimalSkins.Count; i++)
        {
            // Need to define a local variable since i dynamically changes for listeners
            int slotIndex = i;
            Sprite sprite = PlayerData.Instance.currentAnimalSkins[i];
            GameObject slot = (GameObject)Instantiate(Resources.Load("Prefabs/Gachapon/Slot"));
            slot.transform.SetParent(SelectedGrid.transform, false);
            slot.GetComponent<Image>().sprite = sprite;
            slot.GetComponent<Button>().onClick.AddListener(delegate
            {
                SelectionIndex = slotIndex;
                Debug.Log(sprite.name);
                ColorBlock block = slot.GetComponent<Button>().colors;
                block.selectedColor = Color.green;
                slot.GetComponent<Button>().colors = block;
            });
            SelectionSlots.Add(slot);
        }
    }

    public void SetupCollection()
    {
        foreach (KeyValuePair<AnimalData, int> data in PlayerData.Instance.acquiredAnimals)
        {
            GameObject slot = (GameObject)Instantiate(Resources.Load("Prefabs/Gachapon/Slot"));
            slot.transform.SetParent(CollectionGrid.transform, false);
            slot.GetComponent<Image>().sprite = data.Key.sprite;
            slot.GetComponent<Button>().onClick.AddListener(delegate
            {
                CurrentAnimalText.text = data.Key.animalName;
                CurrentAnimalImage.sprite = data.Key.sprite;
                AnimalCountText.text = data.Value.ToString();
            });
        }
    }

    public void SetSelectedAnimal()
    {
        PlayerData.Instance.currentAnimalSkins[SelectionIndex] = CurrentAnimalImage.sprite;
        SelectionSlots[SelectionIndex].GetComponent<Image>().sprite = CurrentAnimalImage.sprite;
    }

    public void CleanUpCollection()
    {
        foreach (Transform child in CollectionGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

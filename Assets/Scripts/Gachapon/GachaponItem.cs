using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaponItem : MonoBehaviour
{
    [SerializeField]
    private Image gachaImage;

    [SerializeField]
    private Material shield;

    [SerializeField]
    private float floatThreshold;

    [SerializeField]
    private float speed;

    private RectTransform rectTransform;


    // Float boundaries
    private float upperYBound;
    private float lowerYBound;
    private float initialYPos;
    private bool floatingUp;

    // Shield Stuff
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    private float shieldStep;
    public GameObject TheShield;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        initialYPos = rectTransform.position.y;
        upperYBound = rectTransform.position.y + floatThreshold;
        lowerYBound = rectTransform.position.y - floatThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        float newYPosition;
        if (floatingUp)
        {
            newYPosition = rectTransform.position.y + Time.deltaTime * speed;
        }
        else
        {
            newYPosition = rectTransform.position.y - Time.deltaTime * speed;
        }

        rectTransform.position = new Vector3(rectTransform.position.x, newYPosition, rectTransform.position.z);

        if (IsOutOfBounds())
        {
            floatingUp = !floatingUp;
        }
    }

    public bool IsOutOfBounds()
    {
        float yPos = rectTransform.position.y;
        if (floatingUp)
        {
            return yPos >= upperYBound;
        }
        return yPos <= lowerYBound;
    }

    public void SetSprite(Sprite sprite)
    {
        // Set sprite to be visible if it was previously invisible
        if (gachaImage.color.a == 0)
        {
            DisplayImage();
        }
        gachaImage.sprite = sprite;
    }

    public void InitializeImage()
    {
        if (_renderer == null || _propBlock == null)
        {
            // Setup shield
            _propBlock = new MaterialPropertyBlock();
            _renderer = TheShield.GetComponent<Renderer>();
        }

        // Set shield to be empty
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat("_EdgeDisolve", 1);
        _renderer.SetPropertyBlock(_propBlock);

        // Set sprite to be invisible
        gachaImage.color = new Color32(255, 255, 255, 0);
    }

    public void DisplayImage()
    {
        // Set shield to be full
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat("_EdgeDisolve", 0);
        _renderer.SetPropertyBlock(_propBlock);

        // Set sprite to be visible
        gachaImage.color = new Color32(255, 255, 255, 255);
    }
}
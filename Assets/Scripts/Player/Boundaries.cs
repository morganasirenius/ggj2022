using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The boundaries component ensures that the game object stays within the bounds of the main camera,
// preventing the game object from going off screen.
public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;  // The main bounds of the current screen

    private float objectWidth;     // The width of the sprite
    private float objectHeight;    // The height of the sprite


    void Start()
    {
        // Get the edge points of the camera by converting the screen dimensions to world transformation points
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Set the object's width/height in order to pad the clamping and avoid clipping the player off screen
        // We use half the actual bounds because clamps are based off of the transform's center
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Call LateUpdate to allow movement actions (usually decided in update) to occur first
    void LateUpdate()
    {
        Vector3 currentPos = transform.position;

        // Clamp the existing position between the edges of the screen + the object's width or height
        // This makes sure that if the position is off the bounds we reset the position properly within the bounds
        currentPos.x = Mathf.Clamp(currentPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        currentPos.y = Mathf.Clamp(currentPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        transform.position = currentPos;
    }
}

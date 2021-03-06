using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingButton : MonoBehaviour
{
    // The speed of the rotation
    [SerializeField]
    public float speed;
    // The threshold to stop rotating the button if the mouse is not on it
    [SerializeField]
    public float rotatingThreshold;
    [SerializeField]
    public GameObject button;

    private float initialRotation;

    private bool rotating;

    void Start()
    {
        initialRotation = button.transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating || !IsInOriginalPosition())
        {
            button.transform.Rotate(Vector3.up * (speed * Time.deltaTime));
        }
    }

    public void SetRotate(bool rotate)
    {
        rotating = rotate;
    }

    public bool IsInOriginalPosition()
    {
        float yRot = button.transform.rotation.y;
        if (yRot < initialRotation)
        {
            return yRot + rotatingThreshold >= initialRotation;
        }
        return yRot - rotatingThreshold <= initialRotation;
    }
}

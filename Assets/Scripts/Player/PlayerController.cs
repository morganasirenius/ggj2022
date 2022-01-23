using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerControls playerControls;
    [SerializeField]
    private float movementVelocity = 3f;
    public LightBeam lightBeam;
    public DarkBeam darkBeam;
    private Quaternion rotation;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Space.LightBeam.performed -= LightBeam;
        playerControls.Space.Shoot.performed -= EnableDarkBeam;
        playerControls.Space.Shoot.canceled -= DisableDarkBeam;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerControls.Space.LightBeam.performed += LightBeam;
        playerControls.Space.Shoot.performed += EnableDarkBeam;
        playerControls.Space.Shoot.canceled += DisableDarkBeam;
    }

    private void LightBeam(InputAction.CallbackContext context)
    {
        Debug.Log("LIGHT");
        //lightBeam.EnableLaser();
        lightBeam.Shoot();
    }

    private void DisableDarkBeam(InputAction.CallbackContext context)
    {
        darkBeam.DisableLaser();

    }
    private void EnableDarkBeam(InputAction.CallbackContext context)
    {
        darkBeam.EnableLaser();

    }

    //BUGGY
    // private void RotateToMouse(Vector2 mousePosition)
    // {
    //     Vector3 direction = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     rotation.eulerAngles = new Vector3(0,0,angle);
    //     transform.rotation = rotation;

    // }
    // Update is called once per frame
    private void Update()
    {
        Vector3 movement = playerControls.Space.Move.ReadValue<Vector2>() * movementVelocity;
        transform.position += movement * Time.deltaTime;
        
        if (darkBeam.lineRenderer.enabled)
        {
            Vector2 mousePosition = playerControls.Space.MousePosition.ReadValue<Vector2>();
            darkBeam.UpdateLaser(mousePosition);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{

    [SerializeField]
    private float movementVelocity = 3f;
    [SerializeField]
    private int health;


    public LightBeam lightBeam;
    public List<DarkBeam> darkBeam;
    private PlayerControls playerControls;
    private Quaternion rotation;

    public bool isDead;


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
        lightBeam.Shoot();
    }

    private void DisableDarkBeam(InputAction.CallbackContext context)
    {
        for (int i = 0; i < darkBeam.Count; i++)
        {
            darkBeam[i].DisableLaser();
        }


    }
    private void EnableDarkBeam(InputAction.CallbackContext context)
    {
        for (int i = 0; i < darkBeam.Count; i++)
        {
            darkBeam[i].EnableLaser();
        }
    }
    private void RotateToMouse(Vector2 mousePosition)
    {
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(mousePosition) - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation.eulerAngles = new Vector3(0, 0, angle - 90); //Need to -90 for some reason
        transform.rotation = rotation;

    }
    // Update is called once per frame
    private void Update()
    {
        Vector3 movement = playerControls.Space.Move.ReadValue<Vector2>() * movementVelocity;
        transform.position += movement * Time.deltaTime;

        Vector2 mousePosition = playerControls.Space.MousePosition.ReadValue<Vector2>();
        for (int i = 0; i < darkBeam.Count; i++)
        {
            if (darkBeam[i].lineRenderer.enabled)
            {
                darkBeam[i].UpdateLaser(mousePosition);
            }
        }
        RotateToMouse(mousePosition);
    }

    public void TakeDamage(int damagedTaken)
    {
        health -= damagedTaken;
        Debug.Log(health);
        if (health <= 0)
        {
            //Show end screen or something
            Debug.Log("you deadge");
            Destroy(gameObject);
        }
    }
}

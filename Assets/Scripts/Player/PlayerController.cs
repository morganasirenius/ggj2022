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
    [SerializeField]
    private int nukeCharges = 3;
    [SerializeField]
    private float nukeClearDelay = 0.1f;
    [SerializeField]
    private int numAlliesToNukeCharge = 5;

    public PlayerControls playerControls;
    private Quaternion rotation;

    private int alliesSaved;

    public LightBeam lightBeam;
    public List<DarkBeam> darkBeam;
    public bool isDead;


    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.UpdateHealth(health);
        UIManager.Instance.UpdateNukeCharges(nukeCharges);

        playerControls.Space.LightBeam.performed += EnableLightBeam;
        playerControls.Space.LightBeam.canceled += DisableLightBeam;
        playerControls.Space.Shoot.performed += EnableDarkBeam;
        playerControls.Space.Shoot.canceled += DisableDarkBeam;
        playerControls.Space.Nuke.performed += Nuke;
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
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Space.LightBeam.performed -= EnableLightBeam;
        playerControls.Space.LightBeam.canceled -= DisableLightBeam;
        playerControls.Space.Shoot.performed -= EnableDarkBeam;
        playerControls.Space.Shoot.canceled -= DisableDarkBeam;
    }
    private void EnableLightBeam(InputAction.CallbackContext context)
    {
        lightBeam.EnableLaser();
    }
    private void DisableLightBeam(InputAction.CallbackContext context)
    {
        lightBeam.DisableLaser();
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

    public void TakeDamage(int damagedTaken)
    {
        health -= damagedTaken;
        UIManager.Instance.UpdateHealth(health);
        if (health <= 0)
        {
            //Show end screen or something
            UIManager.Instance.EndScreen();
            gameObject.SetActive(false);
            isDead = true;
        }
    }

    // Updates the number of allies saved for the player
    // If the number of allies saved hits a threshold, add a nuke charge
    public void UpdateAlliesSaved()
    {
        alliesSaved++;
        if (alliesSaved % numAlliesToNukeCharge == 0)
        {
            nukeCharges++;
            UIManager.Instance.UpdateNukeCharges(nukeCharges);
        }
    }

    private void Nuke(InputAction.CallbackContext context)
    {
        if (nukeCharges > 0)
        {
            StartCoroutine(ClearEnemies());
            StartCoroutine(ClearProjectiles());
            nukeCharges--;
            UIManager.Instance.UpdateNukeCharges(nukeCharges);
        }
    }

    IEnumerator ClearEnemies()
    {
        GameObject[] enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] projectilesOnScreen = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject enemyObj in enemiesOnScreen)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            enemy.DestroyEnemy();
            yield return new WaitForSeconds(nukeClearDelay);
        }

    }
    IEnumerator ClearProjectiles()
    {
        GameObject[] projectilesOnScreen = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject projObj in projectilesOnScreen)
        {
            projObj.SetActive(false);
            yield return new WaitForSeconds(nukeClearDelay);
        }
    }
}

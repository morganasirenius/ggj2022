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

    private int nukesGiven;
    [SerializeField]
    private int bonusForNuke = 2000;

    public LightBeam lightBeam;
    public List<DarkBeam> darkBeam;
    public bool isDead;

    private bool darkBeamEnabled;
    private bool lightBeamEnabled;

    // Damage visual effects
    private Material matDefault;
    private SpriteRenderer sr;
    private float materialResetTime = 0.08f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        // Setup renderer and materials for damaging visual effect
        sr = gameObject.GetComponent<SpriteRenderer>();
        matDefault = sr.material;
    }
    // Start is called before the first frame update
    void Start()
    {
        nukeCharges = 3;
        nukesGiven = 1;
        UIManager.Instance.UpdateHealth(health);
        UIManager.Instance.UpdateNukeCharges(nukeCharges);
        // If handheld device, display mobile controls
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            UIManager.Instance.DisplayMobileUI();
            playerControls.Space.LightBeam.performed += ToggleLightBeam;
            playerControls.Space.Shoot.performed += ToggleDarkBeam;
        }
        else
        {
            playerControls.Space.LightBeam.performed += EnableLightBeam;
            playerControls.Space.LightBeam.canceled += DisableLightBeam;
            playerControls.Space.Shoot.performed += EnableDarkBeam;
            playerControls.Space.Shoot.canceled += DisableDarkBeam;
        }
        playerControls.Space.Nuke.performed += Nuke;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 movement = playerControls.Space.Move.ReadValue<Vector2>() * movementVelocity;
        transform.position += movement * Time.deltaTime;

        Vector2 mousePosition = playerControls.Space.MousePosition.ReadValue<Vector2>();
        // Don't make ship rotate for handheld devices
        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            RotateToMouse(mousePosition);
        }
        for (int i = 0; i < darkBeam.Count; i++)
        {
            if (darkBeam[i].lineRenderer.enabled)
            {
                darkBeam[i].UpdateLaser(mousePosition);
            }
        }
        if (PlayerData.Instance.PlayerScore >= bonusForNuke * nukesGiven)
        {
            nukeCharges++;
            nukesGiven++;
            UIManager.Instance.UpdateNukeCharges(nukeCharges);
        }
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

    private void ToggleLightBeam(InputAction.CallbackContext context)
    {
        if (!lightBeamEnabled)
        {
            lightBeam.EnableLaser();
            lightBeamEnabled = true;
        }
        else
        {
            lightBeam.DisableLaser();
            lightBeamEnabled = false;
        }

        if (darkBeamEnabled)
        {
            for (int i = 0; i < darkBeam.Count; i++)
            {
                darkBeam[i].DisableLaser();
            }
            darkBeamEnabled = false;
        }
    }
    private void EnableLightBeam(InputAction.CallbackContext context)
    {
        lightBeam.EnableLaser();
    }
    private void DisableLightBeam(InputAction.CallbackContext context)
    {
        lightBeam.DisableLaser();
    }
    private void ToggleDarkBeam(InputAction.CallbackContext context)
    {
        if (!darkBeamEnabled)
        {
            for (int i = 0; i < darkBeam.Count; i++)
            {
                darkBeam[i].EnableLaser();
            }
            darkBeamEnabled = true;
        }
        else
        {
            for (int i = 0; i < darkBeam.Count; i++)
            {
                darkBeam[i].DisableLaser();
            }
            darkBeamEnabled = false;
        }

        // Turn off light beam if enabled
        if (lightBeamEnabled)
        {
            lightBeam.DisableLaser();
            lightBeamEnabled = false;
        }
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
        sr.material = ResourceManager.Instance.MaterialDictionary["WhiteFlash"];
        if (health <= 0)
        {
            // Show end screen
            UIManager.Instance.EndScreen();
            PlayerData.Instance.UpdateFinalScore();

            // Stop beam audio
            AudioManager.Instance.StopBeamSound();
            AudioManager.Instance.StopDarkBeamSound();

            // Handle game object removal
            gameObject.SetActive(false);
            isDead = true;

            // Explosion Effect
            GameObject explosion = (GameObject)Instantiate(ResourceManager.Instance.ParticleDictionary["Explosion"], transform.position, transform.rotation);
            AudioManager.Instance.PlaySfx("explode-7");
        }
        else
        {
            Invoke("ResetMaterial", materialResetTime);
            AudioManager.Instance.PlaySfx("hit-2");
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

    private void ResetMaterial()
    {
        sr.material = matDefault;
    }
}

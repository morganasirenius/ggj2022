using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Tooltip("Module used to fire the enemy's projectile.")]
    [SerializeField]
    protected ProjectileModule projectileModule;
    [SerializeField]
    private float timeTilDamage = 1;
    [SerializeField]
    private float timeBetweenAttacks;
    // Maximum attacks the enemy can do
    // A max attack of 0 means the enemy can do infinite attacks
    [SerializeField]
    private int maximumAttacks;
    private float currentDamageTime;
    private float currentAttackTime;
    private int currentAttacks;

    private float materialResetTime = 0.08f;

    // Materials
    private Material matDefault;
    private SpriteRenderer sr;
    void Awake()
    {
        InitializeEnemy();
        // Setup renderer and materials for damaging visual effect
        sr = gameObject.GetComponent<SpriteRenderer>();
        matDefault = sr.material;
    }

    private void InitializeEnemy()
    {
        currentDamageTime = timeTilDamage;
        currentAttackTime = timeBetweenAttacks;
        if (maximumAttacks > 0)
        {
            currentAttacks = maximumAttacks;
        }
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // Only attempt to attack if infinite attacks
        // or there are enough current attacks
        if (maximumAttacks <= 0 || currentAttacks > 0)
        {
            currentAttackTime -= Time.deltaTime;
            if (currentAttackTime <= 0)
            {
                Attack();
                currentAttackTime = timeBetweenAttacks;

                if (maximumAttacks > 0)
                {
                    currentAttacks--;
                }
            }
        }
        Move();
    }

    protected override void OnBecameInvisible()
    {
        ResetEnemy();
    }

    public void TakeDamage(int damagedTaken)
    {
        currentDamageTime -= Time.deltaTime;
        if (currentDamageTime <= 0)
        {
            health -= damagedTaken;
            sr.material = ResourceManager.Instance.MaterialDictionary["WhiteFlash"];
            if (health <= 0)
            {
                DestroyEnemy();
            }
            else
            {
                currentDamageTime = timeTilDamage;
                Invoke("ResetMaterial", materialResetTime);
                AudioManager.Instance.PlaySfx("hit-2");
            }
        }
    }

    public void DestroyEnemy()
    {
        GameObject explosion = (GameObject)Instantiate(ResourceManager.Instance.ParticleDictionary["Explosion"], transform.position, transform.rotation);
        AudioManager.Instance.PlaySfx("explode-2", 0.5f);
        AddScore();
        ResetEnemy();
    }
    private void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void ResetEnemy()
    {
        InitializeEnemy();
        ResetMaterial();
        ResetEntity();
    }
}

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
    void Awake()
    {
        currentDamageTime = timeTilDamage;
        currentAttackTime = timeBetweenAttacks;
        if (maximumAttacks > 0)
        {
            currentAttacks = maximumAttacks;
        }
    }
    // Update is called once per frame
    void Update()
    {
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

    public void TakeDamage(int damagedTaken)
    {
        currentDamageTime -= Time.deltaTime;
        if (currentDamageTime <= 0)
        {
            health -= damagedTaken;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                currentDamageTime = timeTilDamage;
            }
        }
    }
}

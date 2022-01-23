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
    private float currentDamageTime;
    private float currentAttackTime;
    void Start()
    {
        currentDamageTime = timeTilDamage;
        currentAttackTime = timeBetweenAttacks;
    }
    // Update is called once per frame
    void Update()
    {
        currentAttackTime -= Time.deltaTime;
        if (currentAttackTime <= 0)
        {
            Attack();
            currentAttackTime = timeBetweenAttacks;
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

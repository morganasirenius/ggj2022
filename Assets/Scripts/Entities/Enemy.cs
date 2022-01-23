using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private float timeTilDamage = 1;
    private float currentTime;

    void Start()
    {
        currentTime = timeTilDamage;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void TakeDamage(int damagedTaken)
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            health -= damagedTaken;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                currentTime = timeTilDamage;
            }
        }
    }
}

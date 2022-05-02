using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected int scoreValue;

    protected int currentHealth;

    public virtual void OnSpawn() { return; }
    public virtual void Attack() { return; }
    public virtual void Move() { return; }
    public virtual void OnDeath() { return; }
    public virtual void OnReset() { return; }

    void Start()
    {
        currentHealth = health;
    }

    protected virtual void OnDisable()
    {
        OnReset();
        ResetEntity();
    }

    protected virtual void OnBecameInvisible()
    {
        OnReset();
        ResetEntity();
    }

    protected void ResetEntity()
    {
        currentHealth = health;
        gameObject.SetActive(false);
    }

    protected void AddScore()
    {
        PlayerData.Instance.AddScore(scoreValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;

    public virtual void OnSpawn() { return; }
    public virtual void Attack() { return; }
    public virtual void Move() { return; }
    public virtual void OnDeath() { return; }

    // Think of better way to despawn when entity goes out of bounds

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnBecameInvisible()
    {
        Destroy();
    }

}

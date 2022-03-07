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

    // private bool hasBeenInvisible;

    public virtual void OnSpawn() { return; }
    public virtual void Attack() { return; }
    public virtual void Move() { return; }
    public virtual void OnDeath() { return; }

    public virtual void OnReset() { return; }

    protected virtual void Update()
    {
        /*
        // If the object is now seen in the camera, it has been invisible before (when it was spawning)
        if (GetComponent<Renderer>().isVisible && hasBeenInvisible == false)
        {
            Debug.Log("Object spawning in the screen!");
            hasBeenInvisible = true;
        }
        else if (hasBeenInvisible == true) // If has been invisble already and is out of the screen, disable the gameobject
        {
            Debug.Log("Object off the screen!");
            gameObject.SetActive(false);
            hasBeenInvisible = false;
        }*/
    }

    protected virtual void OnDisable()
    {
        OnReset();
        ResetEntity();
    }

    protected virtual void OnBecameInvisible()
    {
        Debug.Log("Entity invisible: " + gameObject.name);
        OnReset();
        ResetEntity();
    }

    protected void ResetEntity()
    {
        gameObject.SetActive(false);
        currentHealth = health;
    }

    protected void AddScore()
    {
        Score.Instance.AddScore(scoreValue);
    }
}

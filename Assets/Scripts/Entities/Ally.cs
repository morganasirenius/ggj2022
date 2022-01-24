using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Entity
{
    private float currentTime; // The current time a food is being transformed
    [SerializeField] private float maxTime = 1;
    private bool rescued = false;
    [SerializeField] private Transform target;

   [SerializeField] private float rescueSpeed;
   [SerializeField] private GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rescued)
        {
            transform.position = Vector3.MoveTowards (transform.position, target.position, rescueSpeed * Time.deltaTime);
        }
    }

    public bool Rescue()
    {
        currentTime += Time.deltaTime;
        int timeSeconds = (int)currentTime;
        if (timeSeconds == maxTime)
        {
            Debug.Log("You rescued them yay");
            rescued = true;
            shield.SetActive(true);
        }
        return rescued;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Player" && rescued)
        {
            Destroy(gameObject);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    private float currentTime; // The current time a food is being transformed
    private float maxTime = 1;
    private bool rescued = false;
    [SerializeField] private Transform target;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rescued)
        {
            transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public bool Rescue()
    {
        currentTime += Time.deltaTime;
        int timeSeconds = (int)currentTime;
        Debug.Log(timeSeconds);
        if (timeSeconds == maxTime)
        {
            Debug.Log("You rescued them yay");
            rescued = true;
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

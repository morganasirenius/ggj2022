using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Entity
{
    private float currentTime; // The current time a food is being transformed
    [SerializeField] private float maxTime = 1;
    private bool rescued = false;
    private Transform target;

   [SerializeField] private float rescueSpeed;
   [SerializeField] private Material shield;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
   private float shieldStep;
   public GameObject TheShield;
    // Start is called before the first frame update
    void Start()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = TheShield.GetComponent<Renderer>();
        Debug.Log(_propBlock);
        Debug.Log(_renderer);
        target = PlayerController.Instance.transform;
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
        float percentageTime = 1 - (currentTime / maxTime);
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetFloat("_EdgeDisolve", percentageTime);
        _renderer.SetPropertyBlock(_propBlock);

        if (currentTime >= maxTime)
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

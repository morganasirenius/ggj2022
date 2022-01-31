using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3f;
    Vector3 StartPosition;
    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed  * Time.deltaTime);
        if (transform.position.y < -18f )
        {
            transform.position = StartPosition;
        }
    }
}

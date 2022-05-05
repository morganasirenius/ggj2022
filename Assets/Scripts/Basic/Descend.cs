using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descend : MonoBehaviour
{
    public float Speed;
    public float MinSpeed;
    public float MaxSpeed;
    public float MinScale;
    public float MaxScale;
    private SpriteRenderer spriteRenderer;
    public string SpriteName;
    private List<Sprite> spriteList;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteList = ResourceManager.Instance.ReturnSpaceObjects(SpriteName);
        Reset();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Speed  * Time.deltaTime, Space.World);
        if (transform.position.y < -7f )
        {
            Reset();
        }        
    }

    void Reset()
    {
        float spawnY = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y) + 10;
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        Speed = Random.Range(MinSpeed, MaxSpeed);
        
        spriteRenderer.sprite = spriteList[Random.Range(0, spriteList.Count)];
        float randScale = Random.Range(MinScale, MaxScale);
        transform.Rotate (0,0,Random.Range(0, 360));
        gameObject.transform.localScale = new Vector3(randScale, randScale, 0);
        transform.position = new Vector2(spawnX, spawnY);
    }
}

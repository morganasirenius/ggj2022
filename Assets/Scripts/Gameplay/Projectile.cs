using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float moveSpeed;
    private Vector2 moveDirection;
    private int damage;
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Have some way to trigger Destroy() after the projectile goes out of bounds
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void Destroy()
    {
        // Don't actually destroy, disable so it can be reused by the object pooler
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.Instance.TakeDamage(damage);
            Destroy();
        }
    }
}

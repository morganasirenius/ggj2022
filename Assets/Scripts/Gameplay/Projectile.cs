using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float moveSpeed;
    private Vector2 moveDirection;
    private int damage;

    // Update is called once per frame
    void Update()
    {
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.Instance.TakeDamage(damage);
            Destroy();
        }
    }

    void OnBecameInvisible()
    {
        Destroy();
    }
}

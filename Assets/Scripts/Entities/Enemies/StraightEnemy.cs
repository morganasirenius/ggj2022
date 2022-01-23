using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightEnemy : Enemy
{
    [SerializeField]
    private Globals.Direction moveDirection;
    private Vector3 direction;

    void Start()
    {
        switch (moveDirection)
        {
            case Globals.Direction.Up:
                direction = Vector3.up;
                break;
            case Globals.Direction.Down:
                direction = Vector3.down;
                break;
            case Globals.Direction.Left:
                direction = Vector3.left;
                break;
            case Globals.Direction.Right:
                direction = Vector3.right;
                break;
            default:
                break;
        }

    }
    public override void Move()
    {
        base.Move();
        this.transform.position += direction * speed;
    }
}

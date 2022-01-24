using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveEnemy : Enemy
{
    private Vector3 startPosition;
    private Vector3 peakPosition;
    private Vector3 endPosition;
    private float currentInterpolationPoint;
    [SerializeField]
    private Globals.Direction moveDirection;

    // The distance relative to the enemy's position the enemy will travel to
    // Technical: The y-axis to add to the current position to define the end position
    [SerializeField]
    private float travelDistance;
    // How far into the screen the enemy will travel before returning
    // Technical: The height of the bezier curve
    [SerializeField]
    private float travelHeight;

    // Define direction of the curve path
    private Vector3 direction;

    public void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position + new Vector3(0, travelDistance, 0);
        currentInterpolationPoint = 0;


        if (moveDirection == Globals.Direction.Left)
        {
            direction = Vector3.left;
        }
        else
        {
            direction = Vector3.right;
        }

        // Calculate the top of the curve by takign the mid distance of the start and end points * some height
        peakPosition = transform.position + (endPosition - transform.position) / 2 + direction * travelHeight;
    }

    void OnEnable()
    {
        Start();
    }
    public override void Move()
    {
        base.Move();
        if (currentInterpolationPoint < 1.0f)
        {
            currentInterpolationPoint += Time.deltaTime * speed;
            this.transform.position = GetQuadraticBezierPosition(currentInterpolationPoint);
        }
    }
    Vector3 GetQuadraticBezierPosition(float t)
    {
        Vector3 p0 = startPosition;
        Vector3 p1 = peakPosition;
        Vector3 p2 = endPosition;
        return Mathf.Pow(1 - t, 2f) * p0 + (2 * (1 - t) * t * p1) + Mathf.Pow(t, 2f) * p2;
    }

    public override void Attack()
    {
        projectileModule.Fire(direction);
    }
}

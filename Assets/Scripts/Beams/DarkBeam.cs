using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBeam : Laser
{
    public override void Shoot()
    {
    }
    public void UpdateLaser(Vector2 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float scale = 25;
        Vector3 offsetPos = mousePosition - new Vector2(transform.position.x, transform.position.y);
        Vector3 newVec = offsetPos.normalized * scale;
        newVec += transform.position;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, newVec);

        Vector2 direction = (Vector2)newVec - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);
        
        if (hit && hit.transform.gameObject.tag == "Enemy" )
        {
            Debug.Log("HITTTTT");
        }
    }
}

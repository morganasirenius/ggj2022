using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : Laser
{
    [SerializeField] private float laserDuration;
    private Vector2 laserReset = new Vector2(0,16);

    public void Update()
    {
        if (lineRenderer.enabled)
        {
            CheckHit();
        }
    }

    public void CheckHit()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, new Vector2(firePoint.position.x, 16));

        Vector2 direction = (Vector2)lineRenderer.GetPosition(1) - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);
        if (hit && hit.transform.gameObject.tag == "Ally" )
        {
            Ally ally = hit.transform.gameObject.GetComponent<Ally>();
            lineRenderer.SetPosition(1, new Vector2(firePoint.position.x, hit.point.y));
            if (ally.Rescue())
            {
                DisableLaser();
            }
            
        }
        else
        {
            StartCoroutine(ShootEffect());
        }
    }
    public override void Shoot()
    {
        EnableLaser();
    }
    protected IEnumerator ShootEffect()
    {
        yield return new WaitForSeconds(laserDuration);
        DisableLaser();
    }
}

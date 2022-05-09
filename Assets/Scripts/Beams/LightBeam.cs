using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : Laser
{
    [SerializeField] private float laserDuration;
    private Vector2 laserReset = new Vector2(0, 16);

    private bool soundPlaying;

    public void Update()
    {
        if (lineRenderer.enabled)
        {
            if (!soundPlaying)
            {
                AudioManager.Instance.PlayBeamSound("light_beam", 0.3f);
                soundPlaying = true;
            }
            CheckHit();
        }
        else
        {
            AudioManager.Instance.StopBeamSound();
            soundPlaying = false;
        }
    }

    public void CheckHit()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, new Vector2(firePoint.position.x, 16));

        Vector2 direction = (Vector2)lineRenderer.GetPosition(1) - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude, LayerMask.GetMask("Ally"));
        if (hit && hit.transform.gameObject.tag == "Ally")
        {
            Ally ally = hit.transform.gameObject.GetComponent<Ally>();
            lineRenderer.SetPosition(1, new Vector2(firePoint.position.x, hit.point.y));
            ally.Rescue();
        }
    }
}

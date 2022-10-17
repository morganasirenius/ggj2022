using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBeam : Laser
{
    [SerializeField] private int damage;

    public GameObject EndVFX;
    private bool soundPlaying;

    void Start()
    {
        for (int i = 0; i < EndVFX.transform.childCount; i++)
        {
            var ps = EndVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null) particles.Add(ps);
        }
        FillLists();
        DisableLaser();
    }
    public void Update()
    {
        if (lineRenderer.enabled)
        {
            if (!soundPlaying)
            {
                AudioManager.Instance.PlayDarkBeamSound("dark_beam");
                soundPlaying = true;
            }
        }
        else
        {
            AudioManager.Instance.StopDarkBeamSound();
            soundPlaying = false;
        }
    }
    public void UpdateLaser(Vector2 mousePosition)
    {
        Vector2 mainPosition;
        float scale = 25;
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            mainPosition = new Vector2(firePoint.position.x, 16);
        }
        else
        {
            mainPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        }
        Vector3 offsetPos = mainPosition - new Vector2(transform.position.x, transform.position.y);
        Vector3 newVec = offsetPos.normalized * scale;
        newVec += transform.position;

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, newVec);

        Vector2 direction = (Vector2)newVec - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude, LayerMask.GetMask("Enemy"));
        if (hit && hit.transform.gameObject.tag == "Enemy")
        {
            lineRenderer.SetPosition(1, hit.point);
            Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        EndVFX.transform.position = lineRenderer.GetPosition(1);
    }
}

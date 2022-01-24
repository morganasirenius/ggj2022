using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public GameObject StartVFX;
    protected List<ParticleSystem> particles = new List<ParticleSystem>();
    void Start()
    {
        FillLists();
        DisableLaser();

    }
    public abstract void Shoot();
    public void EnableLaser()
    {
        lineRenderer.enabled = true;
        EnableParticles();
    }

    public void DisableLaser()
    {
        lineRenderer.enabled = false;
        DisableParticles();
    }
    protected void EnableParticles()
    {
        for (int i=0; i < particles.Count; i++)
        {
            particles[i].Play();
        }
    }

    protected void DisableParticles()
    {
        for (int i=0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }
    protected void FillLists()
    {
        for (int i=0; i < StartVFX.transform.childCount; i++)
        {
            var ps = StartVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null) particles.Add(ps);
        }
    }
}

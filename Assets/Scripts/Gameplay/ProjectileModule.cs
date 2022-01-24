using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class to define specific projectiles
public abstract class ProjectileModule : MonoBehaviour
{
    // The speed of the projectile when fired
    [SerializeField]
    protected float speed;
    // The number of projectiles to spawn
    [SerializeField]
    protected int projectileCount;
    // The time it takes to spawn the next projectile
    // Used primarily for staggering shots, you might
    // want to set this to 0 for spread shots
    [SerializeField]
    protected float timeBetweenProjectiles;
    // The damage the projectile will do
    [SerializeField]
    protected int projectileDamage;
    // The spawnpoint of the projectile
    [SerializeField]
    protected GameObject firePoint;

    // Object pooler associated with the actual projectile object
    protected ObjectPooler projectilePooler;
    protected Vector3 fireDirection;

    void Start()
    {
        // TODO: Choose from different projectiles
        projectilePooler = ResourceManager.Instance.ProjectilePooler;
    }

    public virtual void Fire(Vector3 dir)
    {
        fireDirection = dir;
        StartCoroutine(FireLogic());
    }

    public abstract IEnumerator FireLogic();
}

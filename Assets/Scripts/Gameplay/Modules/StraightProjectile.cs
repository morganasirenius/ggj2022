using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : ProjectileModule
{
    public override IEnumerator FireLogic()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 projDir = fireDirection;

            GameObject proj = projectilePooler.GetPooledObject();
            proj.transform.position = firePoint.transform.position;
            proj.transform.rotation = firePoint.transform.rotation;
            proj.SetActive(true);

            proj.GetComponent<Projectile>().SetMoveSpeed(speed);
            proj.GetComponent<Projectile>().SetMoveDirection(projDir);
            yield return new WaitForSeconds(timeBetweenProjectiles);
        }
    }
}

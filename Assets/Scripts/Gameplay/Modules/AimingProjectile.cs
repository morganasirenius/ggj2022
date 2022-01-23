using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingProjectile : ProjectileModule
{
    public override IEnumerator FireLogic()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 projDirection = (PlayerController.Instance.transform.position - transform.position).normalized;

            GameObject proj = projectilePooler.GetPooledObject();
            proj.transform.position = firePoint.transform.position;
            proj.transform.rotation = firePoint.transform.rotation;
            proj.SetActive(true);

            proj.GetComponent<Projectile>().SetMoveSpeed(speed);
            proj.GetComponent<Projectile>().SetMoveDirection(projDirection);
            yield return new WaitForSeconds(timeBetweenProjectiles);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveProjectile : ProjectileModule
{
    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;
    public override IEnumerator FireLogic()
    {
        float angleStep = (endAngle - startAngle) / projectileCount;
        float angle = startAngle;

        for (int i = 0; i < projectileCount; i++)
        {
            float projDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 projMoveVector = new Vector3(projDirX, projDirY, 0f);
            Vector2 projDir = (projMoveVector - transform.position).normalized;

            GameObject proj = projectilePooler.GetPooledObject();
            proj.transform.position = firePoint.transform.position;
            proj.transform.rotation = firePoint.transform.rotation;
            proj.SetActive(true);

            proj.GetComponent<Projectile>().SetMoveSpeed(speed);
            proj.GetComponent<Projectile>().SetMoveDirection(projDir);
            proj.GetComponent<Projectile>().SetDamage(projectileDamage);
            angle += angleStep;
            yield return new WaitForSeconds(timeBetweenProjectiles);
        }
    }
}

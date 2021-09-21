using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletRaycast 
{
    public static void Shoot(Vector3 shootPos, Vector3 shootDir)
    {
        Debug.DrawLine(shootPos, shootDir, Color.yellow, .1f);
        RaycastHit2D hit = Physics2D.Raycast(shootPos, shootDir);

        Weapon weapon = WeaponHandler.instance.getCurrentWeapon();

        if (hit.collider != null)
        {
            Zombie hitZombie = hit.collider.GetComponent<Zombie>();
            if (hitZombie != null)
            {
                hitZombie.TakeDamage(weapon.Damage);
            }
        }
    }
}

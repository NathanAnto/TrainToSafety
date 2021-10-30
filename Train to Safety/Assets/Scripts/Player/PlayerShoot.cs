using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Player player;
    private ObjectPooler objPooler;
    private WeaponHandler weaponHandler;
    private Transform firePoint;

    // Start is called before the first frame update
    private void Awake()
    {
        player = Player.getPlayerInstance();
        weaponHandler = WeaponHandler.instance;
        player.PlayerWeapon = weaponHandler.getCurrentWeapon();
        objPooler = ObjectPooler.instance;
        firePoint = player.PlayerWeapon.transform.GetChild(0).transform;
    }

    private void Update()
    {
        if(objPooler == null) objPooler = ObjectPooler.instance;
        
        // On left mouse click
        if(Input.GetButtonDown("Fire1"))
            Attack();
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    private void Attack()
    {
        var firePointPos = firePoint.position;
        
        Vector3 mouseOnScreen = Utils.GetMouseWorldPosition();
        Vector3 shootDir = (mouseOnScreen - firePointPos).normalized;

        bool canAttack = false;

        // Reduce ammo size
        player.PlayerWeapon.changeValue(ref canAttack);

        if (canAttack)
        {
            Debug.Log("Shoot");
            BulletRaycast.Shoot(firePointPos, shootDir);
            Effect();
        }
    }

    private void Reload()
    {
        player.PlayerWeapon.PlayReload();
    }

    private void Effect()
    {
        objPooler.SpawnFromPool("bullets", firePoint.position, firePoint.rotation);
    }
}

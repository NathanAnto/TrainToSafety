using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Transform firePoint;
    private Weapon playerWeapon;
    private WeaponHandler weaponHandler;

    // Start is called before the first frame update
    private void Start() {
        weaponHandler = WeaponHandler.instance;
        playerWeapon = weaponHandler.GETCurrentWeapon();
        firePoint = playerWeapon.transform.GetChild(0).transform;
    }

    private void Update() {
        CheckShootInput();
    }

    private void CheckShootInput()
    {
        // On left mouse click
        if(Input.GetButtonDown("Fire1"))
            Attack();
        // On 'R' button pressed
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
        // Change weapon tester
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerWeapon = weaponHandler.SelectNextWeapon();
            GetComponent<PlayerMove>().playerSpriteRenderer.ResetRenderers(
                new List<Transform>(){
                    playerWeapon.transform, // Weapon
                    playerWeapon.transform.GetChild(1).transform, // Hand 1
                    playerWeapon.transform.GetChild(2).transform // Hand 2
                });
        }
    }

    private void Attack() {
        var firePointPos = firePoint.position;
        
        Vector3 mouseOnScreen = Utils.GetMouseWorldPosition();
        Vector3 shootDir = (mouseOnScreen - firePointPos).normalized;

        var canAttack = false;

        // Reduce ammo size
        playerWeapon.changeValue(ref canAttack);

        if (canAttack) {
            Debug.Log("Shoot");
            BulletRaycast.Shoot(firePointPos, shootDir);
            Effect();
        }
    }

    private void Reload() {
        playerWeapon.PlayReload();
    }

    private void Effect() {
        ObjectPooler.instance.SpawnFromPool("bullets", firePoint.position, firePoint.rotation);
    }
}

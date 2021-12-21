using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Transform firePoint;
    private Weapon playerWeapon;
    private WeaponHandler weaponHandler;
    private InputManager inputManager;

    // Start is called before the first frame update
    private void Start() {
        weaponHandler = WeaponHandler.instance;
        playerWeapon = weaponHandler.GETCurrentWeapon();
        firePoint = playerWeapon.transform.GetChild(0).transform;
        inputManager = GetComponent<InputManager>();
    }

    private void Update() {
        // On left mouse click
        inputManager.OnShoot += Attack;
        
        // On 'R' button pressed
        inputManager.OnReload += Reload;
        
        // Change weapon tester
        inputManager.OnSwitch += SwitchWeapon; // Slow, works every 1/5

        // Works, but ugly
        // if (Input.GetKeyDown(KeyCode.Q)) {
        //     playerWeapon = weaponHandler.SelectNextWeapon();
        //     GetComponent<PlayerMove>().playerSpriteRenderer.ResetRenderers(playerWeapon.transform);
        // }
    }

    private void Attack() {
        Vector3 mouseOnScreen = Utils.GetMouseWorldPosition();
        Vector3 shootDir = (mouseOnScreen - firePoint.position).normalized;

        // Reduce ammo size
        playerWeapon.Attack(firePoint, shootDir);
    }

    private void Reload() {
        playerWeapon.PlayReload();
    }

    private void SwitchWeapon() {
        Debug.Log("Switching");
        playerWeapon = weaponHandler.SelectNextWeapon();
        GetComponent<PlayerMove>().playerSpriteRenderer.ResetRenderers(playerWeapon.transform);
    }
}

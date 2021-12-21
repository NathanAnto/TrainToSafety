using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Ranged : Weapon
{
    [SerializeField] private int ammo;
    [SerializeField] private int magSize;
    [SerializeField] private int damage;
    [SerializeField] private int attackRate;
    
    private float nextFire;
    private int maxAmmo;
    private int maxMagSize;
    private bool canAttack = true;
    private bool reloading;
    private Animator animator;
    private static readonly int Reloading = Animator.StringToHash("Reloading");

    private void Start() {
        maxAmmo = ammo;
        maxMagSize = magSize;
        Damage = damage;
        AttackRate = attackRate;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() => animator.SetBool("Reloading", reloading);

    // TODO Create Bolt action class for shotgun and sniper weapons
    /// <summary>
    /// When attack is triggered
    /// </summary>
    /// <param name="firePoint"></param>
    /// <param name="shootDir"></param>
    /// <returns>void</returns>
    public override void Attack(Transform firePoint, Vector3 shootDir)
    {
        // On left mouse click
        if (canAttack && !reloading) {
            Debug.Log("Shoot");
            nextFire = Time.time + attackRate;
            magSize--;
            
            var position = firePoint.position;
            BulletRaycast.Shoot(position, shootDir);
            ObjectPooler.instance.SpawnFromPool("bullets", position, firePoint.rotation);
            animator.SetTrigger("Shoot");
            
            // if mag is empty
            if(magSize <= 0) {
                ammo -= maxMagSize;
                // TODO Stop weird shooting after reloading an empty clip
                PlayReload();
            }
            // if ammo is empty
            else if(ammo <= 0) {
                Debug.Log("Switching weapon...");
                // weaponHandler.selectNextWeapon();
                // Debug.Log($"Weapon {weaponHandler.getCurrentWeapon().name}");
                SetMaxAmmo();
            }
        }
        
        canAttack = Time.time > nextFire;
    }

    /// <summary>
    /// Play reload animation if needed
    /// </summary>
    public override void PlayReload()
    {
        if (magSize < maxMagSize) {
            reloading = true;
        }
    }
    
    /// <summary>
    /// Called after every reload
    /// </summary>
    private void IncrementReloadCount() {
        magSize++;
        // If can reload
        if (magSize < maxMagSize) {
            reloading = true;
        } else {
            ////Debug.Log("Reloaded full mag");
            animator.Play("Shotgun_action");
        }
    }

    /// <summary>
    /// When reloading action is over
    /// </summary>
    private void ActivateShoot() {
        reloading = false;
    }

    private void SetMaxAmmo() {
        ammo = maxAmmo;
    }
}

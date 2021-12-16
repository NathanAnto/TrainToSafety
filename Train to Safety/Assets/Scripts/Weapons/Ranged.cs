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
    private Animator animator;
    private static readonly int Reloading = Animator.StringToHash("Reloading");
    private static readonly int CanShoot = Animator.StringToHash("CanShoot");

    private void Start() {
        maxAmmo = ammo;
        maxMagSize = magSize;
        Damage = damage;
        AttackRate = attackRate;
        animator = GetComponent<Animator>();
    }

    public override void changeValue(ref bool canAttack)
    {
        if (animator.GetBool(Reloading) && !animator.GetBool(CanShoot)) return;
        canAttack = Time.time > nextFire;
        
        // On left mouse click
        if (canAttack) {
            animator.SetTrigger("Shoot");
            nextFire = Time.time + attackRate;
            magSize--;
            if(magSize <= 0) {
                ammo -= maxMagSize; 
                PlayReload();
            } else if(ammo <= 0) {
                Debug.Log("Switching weapon...");
                // weaponHandler.selectNextWeapon();
                // Debug.Log($"Weapon {weaponHandler.getCurrentWeapon().name}");
                SetMaxAmmo();
            }
        }
    }

    public override void PlayReload()
    {
        if (!animator.GetBool(Reloading) && magSize < maxMagSize) {
            Debug.Log("Starting reload");
            animator.SetBool(Reloading, true);
            animator.SetBool(CanShoot, false);
        }
    }

    private void IncrementReloadCount()
    {
        magSize++;
        // If can reload
        if (magSize < maxMagSize) {
            animator.Play("Shotgun_reload");
        } else {
            Debug.Log("Reloaded full mag");
            animator.Play("Shotgun_action");
            animator.SetBool(Reloading, false);
        }
    }

    // TODO: Activate shoot when after reload and action animation
    private void ActivateShoot() {
        Debug.Log("Reactivating shooting");
        animator.SetBool(CanShoot, true);
    }
    
    private void SetMaxAmmo() {
        ammo = maxAmmo;
    }
}

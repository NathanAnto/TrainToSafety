using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Weapon
{
    [SerializeField] private int ammo;
    [SerializeField] private int magSize;
    [SerializeField] private int damage;
    [SerializeField] private int attackRate;
    
    private float nextFire = 0f;
    private bool canShoot = false;
    
    private int maxAmmo;
    private int maxMagSize;
    private Animator animator;
    private Player player;
    private WeaponHandler weaponHandler;

    private void Start() {
        player = Player.getPlayerInstance();
        maxAmmo = ammo;
        maxMagSize = magSize;
        weaponHandler = WeaponHandler.instance;
        
        Damage = damage;
        AttackRate = attackRate;
        animator = GetComponent<Animator>();
    }

    public override void changeValue(ref bool canAttack)
    {
        canAttack = Time.time > nextFire;
        
        // On left mouse click
        if (canAttack) // Input.GetButtonDown("Fire1")
        {
            animator.SetTrigger("Shoot");
            nextFire = Time.time + attackRate;
            ammo--;
            magSize--;
            if(magSize <= 0) {
                player.State = PlayerState.reloading;
                Debug.Log("Reloading...");
                magSize = maxMagSize;
            }
            else if(ammo <= 0) {
                Debug.Log("Switching weapon...");
                weaponHandler.selectNextWeapon();
                Debug.Log($"Weapon {weaponHandler.getCurrentWeapon().name}");
                setMaxAmmo();
            }
        }
    }

    private void setMaxAmmo() {
        ammo = maxAmmo;
    }
}

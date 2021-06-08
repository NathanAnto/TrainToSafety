using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Weapon
{
    public int ammo;
    public int magSize;
    [SerializeField] private GameObject bulletPrefab;
    
    private int maxAmmo;
    private int maxMagSize;
    private Player player;

    private void Start() {
        player = Player.getPlayerInstance();
        maxAmmo = ammo;
        maxMagSize = magSize;
    }

    public Ranged(int damage, float attackRate) {
        Damage = damage;
        AttackRate = attackRate;
    }
        
    public void attack() {
        Debug.Log("Shooting");
    }

    public override void changeValue() {
        ammo--;
        magSize--;
        if(magSize <= 0) {
            player.State = PlayerState.reloading;
            Debug.Log("Reloading...");
            magSize = maxMagSize;
        }
        else if(ammo <= 0) {
            Debug.Log("Switching weapon...");
            WeaponHandler.selectNextWeapon();
            Debug.Log($"Weapon {WeaponHandler.getCurrentWeapon().name}");
            setMaxAmmo();
        }

    }

    private void setMaxAmmo() {
        ammo = maxAmmo;
    }
}

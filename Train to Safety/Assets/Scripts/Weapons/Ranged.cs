using System.Collections;
using System.Collections.Generic;
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
    private int loopCount;

    private void Start() {
        maxAmmo = ammo;
        maxMagSize = magSize;
        
        Damage = damage;
        AttackRate = attackRate;
        animator = GetComponent<Animator>();
    }

    public override void changeValue(ref bool canAttack)
    {
        canAttack = Time.time > nextFire;
        
        // On left mouse click
        if (canAttack) {
            animator.SetTrigger("Shoot");
            nextFire = Time.time + attackRate;
            magSize--;
            if(magSize <= 0) {
                magSize = maxMagSize;
                ammo -= maxMagSize; 
                StartCoroutine(PlayReloadAnim(maxMagSize));
                Debug.Log("Reloading...");
            } else if(ammo <= 0) {
                Debug.Log("Switching weapon...");
                // weaponHandler.selectNextWeapon();
                // Debug.Log($"Weapon {weaponHandler.getCurrentWeapon().name}");
                setMaxAmmo();
            }
        }
    }

    public override void PlayReload()
    {
        if(magSize != maxMagSize)
            StartCoroutine(PlayReloadAnim(maxMagSize-magSize));
    }
    
    private IEnumerator PlayReloadAnim(int reloadCount)
    {
        animator.SetBool("Reloading", true);
        while (loopCount < reloadCount)
        {
            yield return null;
        }
        magSize = maxMagSize;
        animator.SetBool("Reloading", false);
        loopCount = 0;
    }

    public void IncrementLoopCount()
    {
        loopCount++;
        Debug.Log("RELOAD LOOP COUNT " + loopCount);
    }
    
    private void setMaxAmmo() {
        ammo = maxAmmo;
    }
}

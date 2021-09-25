using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private static int selectedWeapon;
    private Weapon currentWeapon;
    public static WeaponHandler instance;

    private void Awake()
    {
        instance = this;
        Player.getPlayerInstance().PlayerWeapon = getCurrentWeapon();
        currentWeapon = transform.GetChild(0).GetComponent<Weapon>();
    }
    
    public Weapon getCurrentWeapon() {
        return currentWeapon;
    }

    public void selectNextWeapon() {
        selectedWeapon++;
        Player.getPlayerInstance().PlayerWeapon = getCurrentWeapon();
    }

    private static void activateWeapon() {
       int i = 0;    
        foreach(Transform weapon in GameObject.Find("Gun").transform)
        {
            weapon.gameObject.SetActive(i == selectedWeapon);
            i++;
        }
    }
}

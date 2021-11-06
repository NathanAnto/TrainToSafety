using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    public static WeaponHandler instance;
    private List<Weapon> weapons = new List<Weapon>();

    private int selectedWeapon;

    private void Awake() {
        instance = this;
        foreach (Transform child in transform) {
            var weapon = child.GetComponent<Weapon>();
            weapons.Add(weapon);
            if (weapon.gameObject.activeSelf) selectedWeapon = weapons.IndexOf(weapon);
            Debug.Log("Selected weapon " + selectedWeapon);
        }

        Debug.Log("index " + selectedWeapon);
        currentWeapon = weapons[selectedWeapon];
    }
    
    public Weapon GETCurrentWeapon() {
        Debug.Log("Weapon "+currentWeapon);
        return currentWeapon;
    }

    // Notify of weapon change
    public Weapon SelectNextWeapon() {
        Debug.Log("Getting next weapon");
        
        weapons[selectedWeapon].gameObject.SetActive(false);
        selectedWeapon++;
        // If list count passed, reset to beginning
        if (selectedWeapon > weapons.Count - 1)
            selectedWeapon = 0;
        
        Debug.Log("Selected weapon " + selectedWeapon);
        weapons[selectedWeapon].gameObject.SetActive(true);
        
        return weapons[selectedWeapon];
    }
}
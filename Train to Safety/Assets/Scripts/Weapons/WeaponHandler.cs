using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private static int selectedWeapon = 0;
    
    private void Start() {
        Player.getPlayerInstance().PlayerWeapon = getCurrentWeapon();
    }
    
    public static Weapon getCurrentWeapon() {
        activateWeapon();
        return GameObject.Find("Weapon").transform.GetChild(selectedWeapon).GetComponent<Weapon>();
    }

    public static void selectNextWeapon() {
        selectedWeapon++;
        Player.getPlayerInstance().PlayerWeapon = getCurrentWeapon();
        activateWeapon();
    }

    private static void activateWeapon() {
       int i = 0;    
        foreach(Transform weapon in GameObject.Find("Weapon").transform) {
            if(i == selectedWeapon)
                weapon.gameObject.SetActive(true);            
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}

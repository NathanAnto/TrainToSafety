using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponListener
{
    public Weapon Weapon { get; private set; }
    
    public Weapon UpdateWeapon(Weapon w)
    {
        Weapon = w;
        return w;
    }
}

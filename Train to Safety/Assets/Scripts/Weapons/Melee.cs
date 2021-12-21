using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [SerializeField] private float durability;
    [SerializeField] private float range;

    public Melee(int damage, float attackRate) {
        Damage = damage;
        AttackRate = attackRate;
    }

    public void attack() {
        Debug.Log("Slashing");
    }

    public override void Attack(Transform firePos, Vector3 attackDir) {
        durability--;
    }
}

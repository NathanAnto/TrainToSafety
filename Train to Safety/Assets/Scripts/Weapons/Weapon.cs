using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int damage { get; set; }
    [SerializeField] private float attackRate { get; set; }

    protected int Damage {
        get { return damage; }
        set {
            damage = value;
        }
    }

    protected float AttackRate {
        get { return attackRate; }
        set {
            attackRate = value;
        }
    }

    public abstract void changeValue();
}

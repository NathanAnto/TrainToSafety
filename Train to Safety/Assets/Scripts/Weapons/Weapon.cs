using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int damage { get; set; }
    [SerializeField] private float attackRate { get; set; }

    public int Damage {
        get => damage;
        set => damage = value;
    }

    public float AttackRate {
        get { return attackRate; }
        set {
            attackRate = value;
        }
    }

    public abstract void Attack(Transform firePos, Vector3 shootDir);
    public virtual void PlayReload() { }
    
}

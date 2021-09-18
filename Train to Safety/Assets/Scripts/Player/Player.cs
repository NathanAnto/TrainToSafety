using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class
public class Player {

    private static Player playerInstance;

   	private float speed { get; set; }
	private float defaultSpeed { get; }
	private int health { get; set; }
	private bool facingRight { get; set; }
    private PlayerState state { get; set; }
	public Weapon weapon { get; set; }
	private GameObject bullet { get; set; }

	private Rigidbody2D rb { get; set; }
	private Animator topAnimator { get; set; }
    private Animator bottomAnimator { get; set; }
    private Animator gunAnimator { get; set; }

    private Player(float speed) {
        this.speed = speed;
        defaultSpeed = speed;
    }

    public static Player getPlayerInstance()  {
        if(playerInstance == null)
            playerInstance = new Player(5);

        return playerInstance;
    }

    // Getters & setters
    public float Speed {
        get => this.speed;
        set => this.speed = value;
    }

    public float DefaultSpeed => this.defaultSpeed;

    public int Health {
        get => this.health;
        set => this.health = value;
    }

    public PlayerState State {
        get => this.state;
        set => this.state = value;
    }

    public bool FacingRight {        
        get => this.facingRight;
        set => this.facingRight = value;
    }

    public Rigidbody2D Rb2d{        
        get => this.rb;
        set => this.rb = value;
    }

    public Animator TopAnimator {        
        get => this.topAnimator;
        set => this.topAnimator = value;
    }
    public Animator BottomAnimator {        
        get => this.bottomAnimator;
        set => this.bottomAnimator = value;
    }
    public Animator GunAnimator {        
        get => this.gunAnimator;
        set => this.gunAnimator = value;
    }

    public Weapon PlayerWeapon {
        get => this.weapon;
        set => this.weapon = value;
        // this.bullet = Ranged.Bullet;
    }
}

public enum PlayerState {
    idle,
    moving,
    aiming,
    attacking,
    reloading,
}
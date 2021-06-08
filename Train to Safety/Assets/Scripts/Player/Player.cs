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
	private Animator animator { get; set; }

    private Player(float speed) {
        this.speed = speed;
        defaultSpeed = speed;
    }

    public static Player getPlayerInstance()  {
        if(playerInstance == null)
            playerInstance = new Player(2);

        return playerInstance;
    }

    // Getters & setters
    public float Speed {
        get { return this.speed; }
        set {
            this.speed = value;
        }
    }

    public float DefaultSpeed {
        get { return this.defaultSpeed; }
    }    
    
    public int Health {
        get { return this.health; }
        set {
            this.health = value;
        }
    }

    public PlayerState State {
        get { return this.state; }
        set {
            this.state = value;
        }
    }

    public bool FacingRight {        
        get { return this.facingRight; }
        set {
            this.facingRight = value;
        }
    }

    public Rigidbody2D Rb2d{        
        get { return this.rb; }
        set {
            this.rb = value;
        }
    }

    public Animator Anim {        
        get { return this.animator; }
        set {
            this.animator = value;
        }
    }

    public Weapon PlayerWeapon {
        get { return this.weapon; }
        set {
            this.weapon = value;
            // this.bullet = Ranged.Bullet;
        }
    }
}

public enum PlayerState {
    idle,
    moving,
    aiming,
    attacking,
    reloading,
}
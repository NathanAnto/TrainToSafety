using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class
public class Player {

    private static Player playerInstance;

    public float DefaultSpeed { get; private set; }
	private bool facingRight { get; set; }
    private PlayerState state { get; set; }
    private Weapon weapon { get; set; }
    private HealthSystem healthSystem { get; set; }
    private MovementSystem movementSystem { get; set; }

    private Player(float defaultSpeed)
    {
        DefaultSpeed = defaultSpeed;
        Speed = DefaultSpeed;
    }

    public static Player getPlayerInstance()
    {
        return playerInstance ?? (playerInstance = new Player(5));
    }

    // Getters & setters
    public float Speed { get; set; }

    public HealthSystem HealthSystem {
        get => healthSystem;
        set {
            if(healthSystem == null)
                healthSystem = value;
        }
    }
    
    public MovementSystem MovementSystem {
        get => movementSystem;
        set {
            if(movementSystem == null)
                movementSystem = value;
        }
    }

    public PlayerState State {
        get => state;
        set => state = value;
    }

    public bool FacingRight {
        get => facingRight;
        set => facingRight = value;
    }

    public Weapon PlayerWeapon {
        get => weapon;
        set => weapon = value;
    }
}

public enum PlayerState {
    idle,
    moving,
    reloading,
}
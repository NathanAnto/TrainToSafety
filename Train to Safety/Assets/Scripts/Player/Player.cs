using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class
public class Player {

    private static Player playerInstance;

   	private float speed { get; set; }
	private float defaultSpeed { get; }
	private HealthSystem healthSystem { get; set; }
	private bool facingRight { get; set; }
    private PlayerState state { get; set; }
    private Weapon weapon { get; set; }

    private Player(float speed) {
        defaultSpeed = speed;
    }

    public static Player getPlayerInstance()  {
        if(playerInstance == null)
            playerInstance = new Player(5);

        return playerInstance;
    }

    // Getters & setters
    public float Speed {
        get => speed;
        set => speed = value;
    }

    public float DefaultSpeed => defaultSpeed;

    public HealthSystem HealthSystem {
        get => healthSystem;
        set
        {
            if(healthSystem == null)
                healthSystem = value;
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
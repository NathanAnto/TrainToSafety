using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	private Player player;
	
	private float moveLimiter = 0.7f;
	private float horizontal;
	private float vertical;

	// Start is called before the first frame update
	void Start()
    {
		player = Player.getPlayerInstance();
		player.Rb2d = GetComponent<Rigidbody2D>();
		player.Anim = GetComponent<Animator>();
		player.FacingRight = true;
	}

	void Update()
	{
		// input
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		flipPlayer(horizontal);
	}


    // Fixed Update is called 50 times per second
    void FixedUpdate()
    {
		handleStates();
		handleMovement(horizontal, vertical);
        // Debug.Log(player.State);
	}

	private void handleStates() {
		if (horizontal == 0 && vertical == 0) 
			player.State = PlayerState.idle;
		else player.State = PlayerState.moving;

		player.Anim.SetBool("isMoving", player.State == PlayerState.moving);

		// right mouse click
		if(Input.GetButton("Fire2")) 
			player.State = PlayerState.aiming;
		else if(Input.GetButtonUp("Fire2"))
			player.State = PlayerState.idle;
		player.Anim.SetBool("isAiming", player.State == PlayerState.aiming);
	}

	private void handleMovement(float horizontal, float vertical)
	{
		// slow down animations if aiming and moving
		if(player.State == PlayerState.aiming) {
			player.Anim.SetFloat("speedMultiplier", 0.5f);
			player.Speed = player.DefaultSpeed/2f;
		}
		else {
			player.Anim.SetFloat("speedMultiplier", 1f);
			player.Speed = player.DefaultSpeed;
		}

		if (horizontal != 0 && vertical != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			horizontal *= moveLimiter;
			vertical *= moveLimiter;
		}

		player.Rb2d.velocity = new Vector2(horizontal * player.Speed, vertical * player.Speed);
	}

	private void flipPlayer(float horizontal)
	{
		if (horizontal > 0 && !player.FacingRight || horizontal < 0 && player.FacingRight)
		{
			rotatePlayer();
		}

		if(player.State == PlayerState.aiming || player.State == PlayerState.moving) {						
			// Transforming mouse pos to resemble player pos
    		Vector2 mouseOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 playerPos = transform.localPosition;

			if(	mouseOnScreen.x > playerPos.x && !player.FacingRight ||
				mouseOnScreen.x < playerPos.x && player.FacingRight)				
				rotatePlayer();
		}
	}	
	private void rotatePlayer() {
		player.FacingRight = !player.FacingRight;
		transform.Rotate(0f, 180f, 0f);
	}
}

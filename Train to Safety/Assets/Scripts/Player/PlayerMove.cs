using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	private Player player;
	private float moveLimiter = 0.7f;
	private float horizontal;
	private float vertical;
	private Rigidbody2D rb;
	private Animator animator;

	// Start is called before the first frame update
	void Start()
    {
		player = Player.getPlayerInstance(); // Get player singleton
		rb = GetComponent<Rigidbody2D>();
		animator = transform.GetChild(0).GetComponent<Animator>(); // Get Animator on Body
		player.FacingRight = true;
		player.Speed = player.DefaultSpeed;
    }

	void Update()
	{
		// input
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		FlipPlayer();
	}


    // Fixed Update is called 50 times per second
    void FixedUpdate()
    {
		HandleStates();
		HandleMovement();
    }

	private void HandleStates() {
		if (horizontal == 0 && vertical == 0) 
			player.State = PlayerState.idle;
		else
			player.State = PlayerState.moving;
	}

	private void HandleMovement()
	{
		if (horizontal != 0 && vertical != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			horizontal *= moveLimiter;
			vertical *= moveLimiter;
		}

		if (vertical > 0) 
		{
			animator.SetBool("goingUp", true);
			player.PlayerWeapon.GetComponent<SpriteRenderer>().sortingOrder = 18;
		}
		else if(vertical < 0)
		{
			animator.SetBool("goingUp", false);
			player.PlayerWeapon.GetComponent<SpriteRenderer>().sortingOrder = 22;
		}
		animator.SetBool("isMoving", player.State == PlayerState.moving);
		
		rb.velocity = new Vector2(horizontal * player.Speed, vertical * player.Speed);
	}

	private void FlipPlayer()
	{
		Vector2 mouseOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 playerPos = transform.localPosition;
		
		if(	mouseOnScreen.x > playerPos.x && !player.FacingRight ||
		    mouseOnScreen.x < playerPos.x && player.FacingRight)				
			RotatePlayer();
	}	
	private void RotatePlayer() {
		player.FacingRight = !player.FacingRight;
		transform.Rotate(0f, 180f, 0f);
	}
}

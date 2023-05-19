using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	private int playerNum;

	private float horizontalMove = 0f;
	private bool jump = false;
	private bool crouch = false;

	private bool canMove;

	void Start()
	{
		playerNum = GetComponent<PlayerInfo>().GetPlayerNum;

		// Needed to turn even players around at the start
		if(playerNum % 2 == 0)
		{
			horizontalMove = -0.1f;
			animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
		}
	}

	// Update is called once per frame
	void Update() 
	{
		canMove = MenuManager.instance.CurrentMenuState == MenuState.Game;

		if(canMove)
		{
			horizontalMove = Input.GetAxisRaw("Horizontal" + playerNum) * PlayerManager.instance.GetPlayerInfoByPlayerNum(playerNum).GetCurrentMoveSpeed;
			animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

			// Input to jump
			if(Input.GetButtonDown("Jump" + playerNum))
			{
				jump = true;
				animator.SetBool("IsJumping", true);
			}

			// Input to start/stop crouching
			if(Input.GetButtonDown("Crouch" + playerNum))
				crouch = true;
			else if(Input.GetButtonUp("Crouch" + playerNum))
				crouch = false;
		}
	}

	// Called from Unity Event on Player object
	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
		animator.SetBool("IsFalling", false);
	}

	// Called from Unity Event on Player object
	public void OnCrouching(bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	// Called from Unity Event on Player object
	public void OnFalling()
	{
		animator.SetBool("IsJumping", false);
		animator.SetBool("IsFalling", true);
	}

	void FixedUpdate()
	{
		if(canMove)
		{
			controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
			jump = false;
		}
	}
}

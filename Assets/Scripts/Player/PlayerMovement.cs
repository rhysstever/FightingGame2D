using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	//public Animator animator;

	private int playerNum;

	private float horizontalMove = 0f;
	private bool jump = false;
	private bool crouch = false;

	void Start()
	{
		playerNum = GetComponent<PlayerInfo>().GetPlayerNum;

		// Needed to turn even players around at the start
		if(playerNum % 2 == 0)
		{
			horizontalMove = -0.1f;
			//animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
		}
	}

	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal" + playerNum) * PlayerManager.instance.GetPlayerInfoByPlayerNum(playerNum).GetCurrentMoveSpeed;

		//animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump" + playerNum))
		{
			jump = true;
			//animator.SetBool("IsJumping", true);
		}

		if (Input.GetButtonDown("Crouch" + playerNum))
			crouch = true;
		else if (Input.GetButtonUp("Crouch" + playerNum))
			crouch = false;
	}

	public void OnLanding ()
	{
		//animator.SetBool("IsJumping", false);
	}

	public void OnCrouching (bool isCrouching)
	{
		//animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character2DController : MonoBehaviour
{
    public float movementSpeed = 1;
    public float jumpForce = 1;

    private string playerNum;  
    private Rigidbody2D rb;
    private PlayerInputActions playerInputActions;
    private InputAction movement;
    private InputAction attack;
    private Vector2 movementVec2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerNum = gameObject.name;
        playerInputActions = new PlayerInputActions();        
    }

	private void OnEnable()
	{
        movement = playerNum == "player1" ? playerInputActions.Player.Movement1 : playerInputActions.Player.Movement2;
        movement.Enable();

        attack = playerNum == "player1" ? playerInputActions.Player.Attack1 : playerInputActions.Player.Attack2;
        attack.Enable();
    }

	private void OnDisable()
	{
		movement.Disable();
        attack.Disable();
    }

	private void FixedUpdate()
    {
        // Movement checks
        movementVec2 = movement.ReadValue<Vector2>();
        if(movementVec2.x != 0.0f) Move(movementVec2.x);
        if(movementVec2.y > 0.0f) Jump();
        if(movementVec2.y < 0.0f) Crouch();

        // Attack check
        if(attack.triggered)
            GetComponent<CharacterCombat>().Attack();
    }

    /// <summary>
    /// Moves the player based on a given amount
    /// </summary>
    /// <param name="movementAmount">The amount the player is moved</param>
	private void Move(float movementAmount)
	{
        transform.position += new Vector3(movementAmount, 0, 0) * Time.deltaTime * movementSpeed;
    }

    /// <summary>
    /// Makes the player jump
    /// </summary>
    private void Jump()
	{
        // Checks that the rigidbody is on the ground
        if(Mathf.Abs(rb.velocity.y) < 0.001f)
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void Crouch()
	{
        Debug.Log("Crouched");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Action
{
    Movement,
    Attack,
    Special,
    Select
}

public class Character2DController : MonoBehaviour
{
    public float movementSpeed = 1;
    public float jumpForce = 1;

    private string playerNum;  
    private Rigidbody2D rb;
    private Vector2 movementVec2;
    private bool isReady;

    private PlayerInputActions playerInputActions;
    private Dictionary<Action, InputAction> inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerNum = gameObject.name;
        playerInputActions = new PlayerInputActions();
        inputActions = new Dictionary<Action, InputAction>();
    }

	private void OnEnable()
	{
        // Get input actions based on which player it is
        InputAction movement = playerNum == "player1" ? playerInputActions.Player.Movement1 : playerInputActions.Player.Movement2;
        InputAction attack = playerNum == "player1" ? playerInputActions.Player.Attack1 : playerInputActions.Player.Attack2;
        InputAction special = playerNum == "player1" ? playerInputActions.Player.Special1 : playerInputActions.Player.Special2;
        InputAction select = playerNum == "player1" ? playerInputActions.Player.Select1 : playerInputActions.Player.Select2;

        // Add each input action to the dictionary
        inputActions = new Dictionary<Action, InputAction>();
        inputActions.Add(Action.Movement, movement);
        inputActions.Add(Action.Attack, attack);
        inputActions.Add(Action.Special, special);
        inputActions.Add(Action.Select, select);

        // Enable all input actions
        foreach(InputAction inputAction in inputActions.Values)
            inputAction.Enable();
    }

	private void OnDisable()
    {
        foreach(InputAction inputAction in inputActions.Values)
            inputAction.Disable();
    }

	private void FixedUpdate()
    {
        switch(GameManager.instance.GetCurrentMenuState())
        {
            case MenuState.MainMenu:
                break;
            case MenuState.Select:
                if(GetInputAction(Action.Select).triggered)
                    isReady = true;
                break;
            case MenuState.Game:
                // Check to pause game
                if(GetInputAction(Action.Select).triggered)
                    GameManager.instance.ChangeMenuState(MenuState.Pause);
                // Movement checks
                movementVec2 = inputActions[Action.Movement].ReadValue<Vector2>();
                if(movementVec2.x != 0.0f)
                    Move(movementVec2.x);
                if(movementVec2.y > 0.0f)
                    Jump();
                if(movementVec2.y < 0.0f)
                    Crouch();
                break;
            case MenuState.Pause:
                // Check to unpause game
                if(GetInputAction(Action.Select).triggered)
                    GameManager.instance.ChangeMenuState(MenuState.Game);
                break;
            case MenuState.PostGame:
                break;
        }
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

    /// <summary>
    /// Makes the player crouch/dunk
    /// </summary>
    private void Crouch()
	{
        Debug.Log("Crouched");

        // Change the box collider to be a smaller one
	}

    /// <summary>
    /// A helper method to get an input action of a player
    /// </summary>
    /// <param name="action">The type of input action</param>
    /// <returns>The input action of that action type</returns>
    public InputAction GetInputAction(Action action) { return inputActions[action]; }

    /// <summary>
    /// Gets the isReady field of the player
    /// </summary>
    /// <returns>Whether the player is ready</returns>
    public bool IsReady() { return isReady; }
}

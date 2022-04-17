using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static GameManager instance = null;

    // Awake is called even before start 
    // (I think its at the very beginning of runtime)
    private void Awake()
    {
        // If the reference for this script is null, assign it this script
        if(instance == null)
            instance = this;
        // If the reference is to something else (it already exists)
        // than this is not needed, thus destroy it
        else if(instance != this)
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    private GameObject player1, player2;
    private MenuState currentMenuState;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial menu state
        ChangeMenuState(MenuState.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        // Do menu-specific logic
        switch(currentMenuState)
        {
            case MenuState.MainMenu:
                break;
            case MenuState.Select:
                // Moves to the Game menu state if both players are ready
                if(player1.GetComponent<Character2DController>().IsReady()
                    && player2.GetComponent<Character2DController>().IsReady())
                    ChangeMenuState(MenuState.Game);
                break;
            case MenuState.Game:
                break;
            case MenuState.Pause:
                break;
            case MenuState.PostGame:
                break;
        }
    }

    /// <summary>
    /// Changes the current menu state
    /// </summary>
    /// <param name="newMenuState">The new menu state</param>
    public void ChangeMenuState(MenuState newMenuState)
    {
        // Set new menu state
        currentMenuState = newMenuState;

        // Update UI
        UIManager.instance.ChangeUIState(currentMenuState);
    }

	/// <summary>
	/// Gets the current MenuState of the game
	/// </summary>
	/// <returns>The current MenuState of the game</returns>
	public MenuState GetCurrentMenuState() { return currentMenuState; }

    /// <summary>
    /// Returns a player object
    /// </summary>
    /// <param name="isFirstPlayer">Whether player 1 is being gotten</param>
    /// <returns>Either player 1 or player 2</returns>
    public GameObject GetPlayer(bool isFirstPlayer) { return isFirstPlayer ? player1 : player2; }
}

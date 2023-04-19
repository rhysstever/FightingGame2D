using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    Title,
    CharacterSelect,
    Game,
    Pause,
    GameEnd
}

public class MenuManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static MenuManager instance = null;

    // Awake is called even before start 
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

    private MenuState currentMenuState;

    public MenuState CurrentMenuState { get { return currentMenuState; } }

    // Start is called before the first frame update
    void Start()
    {
        ChangeMenuState(MenuState.Title);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMenuState();
    }

    /// <summary>
    /// Recurring checks/updates made during each menu state
    /// </summary>
    public void UpdateMenuState()
	{
        switch(currentMenuState)
        {
            case MenuState.Title:
                // Pressing the 'Enter' key changes the game to Character Select
                if(Input.GetKeyDown(KeyCode.Return))
                    ChangeMenuState(MenuState.CharacterSelect);
                break;
            case MenuState.CharacterSelect:
                if(Input.GetKeyDown(KeyCode.Return))
                    ChangeMenuState(MenuState.Game);
                break;
            case MenuState.Game:
                // Pressing the 'P' or 'Escape' keys pauses the game
                if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
                    ChangeMenuState(MenuState.Pause);
                
                // The game ends when a player loses all its health
                if(PlayerManager.instance.GetPlayerInfoByPlayerNum(1).GetCurrentHealth <= 0
                    || PlayerManager.instance.GetPlayerInfoByPlayerNum(2).GetCurrentHealth <= 0)
                    ChangeMenuState(MenuState.GameEnd);
                break;
            case MenuState.Pause:
                // Pressing the 'P' or 'Escape' keys resumes the game
                if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
                    ChangeMenuState(MenuState.Game);
                break;
            case MenuState.GameEnd:
                if(Input.GetKeyDown(KeyCode.Return))
                    ChangeMenuState(MenuState.Title);
                break;
        }
    }

    /// <summary>
    /// Perform one-time changes when the menu state changes
    /// </summary>
    /// <param name="newMenuState">The new menu state</param>
    public void ChangeMenuState(MenuState newMenuState)
	{
        switch(newMenuState)
        {
            case MenuState.Title:
                break;
            case MenuState.CharacterSelect:
                break;
            case MenuState.Game:
                break;
            case MenuState.Pause:
                break;
            case MenuState.GameEnd:
                break;
        }

        // Update UI
        UIManager.instance.UpdateMenuStateUI(newMenuState);

        currentMenuState = newMenuState;
	}
}

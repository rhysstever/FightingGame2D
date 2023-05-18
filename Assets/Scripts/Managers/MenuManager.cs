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

    [SerializeField]
    private MenuState currentMenuState;
    private bool isPlayer1Ready = false;
    private bool isPlayer2Ready = false;

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
                CharacterSelect();

                if(isPlayer1Ready && isPlayer2Ready)
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
                isPlayer1Ready = false;
                isPlayer2Ready = false;
                StartupCharacterSelect();
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

    /// <summary>
    /// Selects a character for both players (avoids bugs)
    /// </summary>
    private void StartupCharacterSelect()
	{
        Character firstEnabledCharacter = CharacterPackManager.instance.GetEnabledCharactersList()[0];

        PlayerInfo player1Info = PlayerManager.instance.GetPlayerInfoByPlayerNum(1);
        player1Info.ChangeCharacter(
                firstEnabledCharacter,
                CharacterManager.instance.GetCharacterInfo(firstEnabledCharacter).Sprite);

        PlayerInfo player2Info = PlayerManager.instance.GetPlayerInfoByPlayerNum(2);
        player2Info.ChangeCharacter(
                firstEnabledCharacter,
                CharacterManager.instance.GetCharacterInfo(firstEnabledCharacter).Sprite);
    }

    private void CharacterSelect()
    {
        if(Input.GetKeyDown(KeyCode.E))
            isPlayer1Ready = !isPlayer1Ready;

        if(!isPlayer1Ready)
		{
            if(Input.GetKeyDown(KeyCode.W))
		    {
                PlayerInfo player1Info = PlayerManager.instance.GetPlayerInfoByPlayerNum(1);
                Character newCharacterSelection = CharacterManager.instance.ChangeSelectedCharacter(player1Info, 1);
                player1Info.ChangeCharacter(
                    newCharacterSelection, 
                    CharacterManager.instance.GetCharacterInfo(newCharacterSelection).Sprite);
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                PlayerInfo player1Info = PlayerManager.instance.GetPlayerInfoByPlayerNum(1);
                Character newCharacterSelection = CharacterManager.instance.ChangeSelectedCharacter(player1Info, -1);
                player1Info.ChangeCharacter(
                    newCharacterSelection,
                    CharacterManager.instance.GetCharacterInfo(newCharacterSelection).Sprite);
            }
		}
        

        if(Input.GetKeyDown(KeyCode.Keypad9))
            isPlayer2Ready = !isPlayer2Ready;

        if(!isPlayer2Ready)
		{
            if(Input.GetKeyDown(KeyCode.Keypad8))
            {
                PlayerInfo player2Info = PlayerManager.instance.GetPlayerInfoByPlayerNum(2);
                Character newCharacterSelection = CharacterManager.instance.ChangeSelectedCharacter(player2Info, 1);
                player2Info.ChangeCharacter(
                    newCharacterSelection,
                    CharacterManager.instance.GetCharacterInfo(newCharacterSelection).Sprite);
            }
            else if(Input.GetKeyDown(KeyCode.Keypad5))
            {
                PlayerInfo player2Info = PlayerManager.instance.GetPlayerInfoByPlayerNum(2);
                Character newCharacterSelection = CharacterManager.instance.ChangeSelectedCharacter(player2Info, -1);
                player2Info.ChangeCharacter(
                    newCharacterSelection,
                    CharacterManager.instance.GetCharacterInfo(newCharacterSelection).Sprite);
            }
		}
    }
}

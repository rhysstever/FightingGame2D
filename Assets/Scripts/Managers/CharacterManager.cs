using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum Character
{
    Con,
    Grace,
    Rhys,
    Sam
}

public class CharacterManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static CharacterManager instance = null;

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

    private AnimatorController[] controllers;

	[SerializeField]
	private List<Sprite> idleSpriteFrameZeros;

    private Dictionary<Character, CharacterInfo> characterInfo;

    public CharacterInfo GetCharacterInfo(Character character) { return characterInfo[character]; }

    // Start is called before the first frame update
    void Start()
    {
        controllers = Resources.LoadAll<AnimatorController>("Animations/Controllers");

        SetupCharacterInfoDictionary();
    }

    /// <summary>
    /// Creates Character objects for every index of the Character enum
    /// </summary>
    private void SetupCharacterInfoDictionary()
	{
        // Instaniate dictionary
        characterInfo = new Dictionary<Character, CharacterInfo>();

        // Create CharacterInfo objects and add them to the dictionary
        for(int i = 0; i < controllers.Length; i++)
            characterInfo.Add((Character)i, new CharacterInfo(idleSpriteFrameZeros[i], controllers[i], "", ""));
    }

    /// <summary>
    /// Select a new character based on currently enabled characters
    /// </summary>
    /// <param name="playerInfo">The playerInfo of the player selecting a new character</param>
    /// <param name="direction">The direction of the new selection (1 is forward, -1 is back)</param>
    /// <returns>The Character enum value of the newly selected character</returns>
    public Character ChangeSelectedCharacter(PlayerInfo playerInfo, int direction)
	{
        Character currentCharacter = playerInfo.GetPlayerCharacter;

        // Ensure direction is valid
        if(direction != 1 && direction != -1)
            return currentCharacter;

        Character[] enabledCharacters = CharacterPackManager.instance.GetEnabledCharactersList();

        // Find where in the array of enabled characters, the current selected character is
        int currentIndex = Array.IndexOf(enabledCharacters, currentCharacter);
        // Add the player's direction choice
        currentIndex += direction;

        // If the current character is the first one and the player selects "back",
        // select the last enabled character in the array
        if(currentIndex < 0)    
            return enabledCharacters[enabledCharacters.Length - 1];
        // If the current character is the last one and the player selects "forward",
        // select the first enabled character in the array
        else if(currentIndex >= enabledCharacters.Length)
            return enabledCharacters[0];
        else 
            return enabledCharacters[currentIndex];
	}
}

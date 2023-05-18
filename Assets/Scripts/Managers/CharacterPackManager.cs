using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CharacterPackName
{
    Base,
    Family,
    Friends
}

public class CharacterPackManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static CharacterPackManager instance = null;

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

    private Dictionary<CharacterPackName, CharacterPack> characterPackDictionary;

    // Start is called before the first frame update
    void Start()
    {
        SetupCharacterPacks();

        // Enable Packs
        GetCharacterPack(CharacterPackName.Base).Enable();
        GetCharacterPack(CharacterPackName.Family).Enable();
        //GetCharacterPack(CharacterPackName.Friends).Enable();
    }

    /// <summary>
    /// Creates each Character Pack object 
    /// </summary>
    private void SetupCharacterPacks()
	{
        // Setup dictionary
        characterPackDictionary = new Dictionary<CharacterPackName, CharacterPack>();

        // Base Pack
        characterPackDictionary.Add(
            CharacterPackName.Base, 
            new CharacterPack(CharacterPackName.Base, new[] { Character.Rhys, Character.Grace }));

        // Family Pack - TODO: Add parents?
        characterPackDictionary.Add(
            CharacterPackName.Family, 
            new CharacterPack(CharacterPackName.Family, new[] { Character.Sam, Character.Con }));

        // Friends Pack - TODO: Add characters to when ready
        characterPackDictionary.Add(
            CharacterPackName.Friends, 
            new CharacterPack(CharacterPackName.Friends, new Character[] {  }));
	}

    /// <summary>
    /// Gets a Character Pack from the dictionary
    /// </summary>
    /// <param name="characterPackName">The name of the Character Pack</param>
    /// <returns>The Character Pack object</returns>
    public CharacterPack GetCharacterPack(CharacterPackName characterPackName)
	{
        return characterPackDictionary[characterPackName];
	}

    /// <summary>
    /// Gets all characters that are included in currently enabled packs
    /// </summary>
    /// <returns>An array of all enabled characters</returns>
    public Character[] GetEnabledCharactersList()
	{
        Character[] enabledCharacters = new Character[] { };

        foreach(CharacterPack characterPack in characterPackDictionary.Values)
        {
            if(characterPack.IsEnabled)
                enabledCharacters = enabledCharacters.Union(characterPack.Characters).ToArray();
        }

        return enabledCharacters;
	}
}

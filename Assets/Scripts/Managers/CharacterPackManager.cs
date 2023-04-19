using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterPackName
{
    Base,
    Family,
    Friend1,
    Friend2,
    Friend3,
    Friend4,
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

    private Dictionary<CharacterPackName, Character[]> characterPackDictionary;

    // Start is called before the first frame update
    void Start()
    {
        // Setup dictionary
        characterPackDictionary = new Dictionary<CharacterPackName, Character[]>();
        characterPackDictionary.Add(CharacterPackName.Base, new[] { Character.Rhys, Character.Grace });
        // TODO: Add other packs when characters are ready
    }

    public Character[] GetCharacterPack(CharacterPackName characterPackName)
	{
        return characterPackDictionary[characterPackName];
	}
}

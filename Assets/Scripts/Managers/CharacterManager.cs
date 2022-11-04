using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Rhys,
    Grace,
    Sam,
    Con
}

public class CharacterManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static CharacterManager instance = null;

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
    private Sprite ConSprite, GraceSprite, RhysSprite, SamSprite;

    private Dictionary<Character, CharacterInfo> characterInfo;

    // Start is called before the first frame update
    void Start()
    {
        SetupCharacterInfoDictionary();
    }

    public CharacterInfo GetCharacterInfo(Character character) { return characterInfo[character]; }

    private void SetupCharacterInfoDictionary()
	{
        // Instaniate dictionary
        characterInfo = new Dictionary<Character, CharacterInfo>();

        // Create CharacterInfo objects and add them to the dictionary
        characterInfo.Add(Character.Con, new CharacterInfo(ConSprite, "", ""));
        characterInfo.Add(Character.Grace, new CharacterInfo(GraceSprite, "", ""));
        characterInfo.Add(Character.Rhys, new CharacterInfo(RhysSprite, "", ""));
        characterInfo.Add(Character.Sam, new CharacterInfo(SamSprite, "", ""));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNum
{
    Player1,
    Player2
}

public enum InputType
{
    Horizontal,
    Vertical,
    Attack
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetPlayerSpecificInput(InputType inputType, PlayerNum playerNum)
	{
        switch(inputType)
        {
            case InputType.Horizontal:
                return "Horizontal" + playerNum.ToString().Substring(6);
            case InputType.Vertical:
                return "Jump" + playerNum.ToString().Substring(6);
            case InputType.Attack:
                return "Attack" + playerNum.ToString().Substring(6);
        }

        return null;
	}
}

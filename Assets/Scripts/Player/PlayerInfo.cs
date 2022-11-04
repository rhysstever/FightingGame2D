using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private int playerNum;

    [SerializeField]
    private Character character;

	void Start()
	{
        // Set the gameobject's name
		gameObject.name = "Player" + playerNum;
        // Set the gameobject's sprite
        GetComponent<SpriteRenderer>().sprite = CharacterManager.instance.GetCharacterInfo(character).Sprite;
	}

    public int GetPlayerNum { get { return playerNum; } }
    public Character GetPlayerCharacter { get { return character; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo
{
	private Sprite sprite;
	private string abilityName;
	private string abilityDescription;

	public Sprite Sprite { get { return sprite; } }

    public CharacterInfo(Sprite sprite, string abilityName, string abilityDescription)
	{
		this.sprite = sprite;
		this.abilityName = abilityName;
		this.abilityDescription = abilityDescription;
	}

	public string DisplayAbility()
	{
		return abilityName + ": " + abilityDescription;
	}
}

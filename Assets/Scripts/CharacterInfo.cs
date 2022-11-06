using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterInfo
{
	private Sprite sprite;
	private AnimatorController controller;
	private string abilityName;
	private string abilityDescription;

	public Sprite Sprite { get { return sprite; } }
	public AnimatorController AnimatorController { get { return controller; } }

	public CharacterInfo(Sprite sprite, AnimatorController controller, string abilityName, string abilityDescription)
	{
		this.sprite = sprite;
		this.controller = controller;
		this.abilityName = abilityName;
		this.abilityDescription = abilityDescription;
	}

	public string DisplayAbility()
	{
		return abilityName + ": " + abilityDescription;
	}
}

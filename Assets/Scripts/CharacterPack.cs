using System.Collections;
using System.Collections.Generic;

public class CharacterPack
{
	private CharacterPackName name;
	private bool isEnabled;
	private Character[] characters;

	public CharacterPackName Name { get { return name; } }
	public bool IsEnabled { get { return isEnabled; } }
	public Character[] Characters { get { return characters; } }

	/// <summary>
	/// Creates a Character Pack object
	/// </summary>
	/// <param name="packName">The name of the character pack</param>
	/// <param name="charactersInPack">The characters in this pack</param>
    public CharacterPack (CharacterPackName packName, Character[] charactersInPack)
	{
		name = packName;
		isEnabled = false;
		characters = charactersInPack;
	}

	public void Enable() { isEnabled = true; }
	public void Disable() { isEnabled = false; }
}

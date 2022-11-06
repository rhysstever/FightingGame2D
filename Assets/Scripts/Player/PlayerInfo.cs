using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private int playerNum;

    [SerializeField]
    private Character character;

    private float baseHealth = 20.0f;
    private float currentHealth;
    private float baseMoveSpeed = 40.0f;
    private float currentMoveSpeed;
    private float baseDamage = 5.0f;
    private float currentDamage;
    private float baseAttackSpeed = 2.0f;
    private float currentAttackSpeed;

    private List<Effect> effects;

    // Properties
    public int GetPlayerNum { get { return playerNum; } }
    public Character GetPlayerCharacter { get { return character; } }
    public float GetCurrentHealth { get { return currentHealth; } }
    public float GetCurrentMoveSpeed { get { return currentMoveSpeed; } }
    public float GetCurrentDamage { get { return currentDamage; } }
    public float GetCurrentAttackSpeed { get { return currentAttackSpeed; } }
    public List<Effect> GetEffects { get { return effects; } }

    void Start()
	{
        effects = new List<Effect>();

        // Set the gameobject's name
		gameObject.name = "Player" + playerNum;
        // Set the gameobject's sprite and animator controller
        GetComponent<SpriteRenderer>().sprite = CharacterManager.instance.GetCharacterInfo(character).Sprite;
        GetComponent<Animator>().runtimeAnimatorController = CharacterManager.instance.GetCharacterInfo(character).AnimatorController;
    }

	void FixedUpdate()
	{
		HandleEffects();
	}

    private void HandleEffects()
	{
        // Get the base values
        float health = baseHealth;
        float moveSpeed = baseMoveSpeed;
        float damage = baseDamage;
        float attackSpeed = baseAttackSpeed;

        // Apply each effect's changes
        if(effects.Count > 0)
        {
            foreach(Effect effect in effects)
            {
                effect.Tick(Time.deltaTime);

                switch(effect.EffectType)
                {
                    case EffectType.Buff:
                        health += effect.HealthEffect;
                        moveSpeed += effect.MoveSpeedEffect;
                        damage += effect.DamageEffect;
                        attackSpeed += effect.AttackSpeedEffect;
                        break;
                    case EffectType.Debuff:
                        health -= effect.HealthEffect;
                        moveSpeed -= effect.MoveSpeedEffect;
                        damage -= effect.DamageEffect;
                        attackSpeed -= effect.AttackSpeedEffect;
                        break;
                    case EffectType.Stun:
                        moveSpeed = 0.0f;
                        break;
                }

            }
        }

        // Set the current values
        currentHealth = health;
        currentMoveSpeed = moveSpeed;
        currentDamage = damage;
        currentAttackSpeed = attackSpeed;
    }

    public float LoseHealth(float amount)
	{
        baseHealth -= amount;

        // Check if the player has died
        if(baseHealth <= 0)
            gameObject.SetActive(false);

        return baseHealth;
    }

    public void AddEffect(Effect effect)
	{
        effects.Add(effect);
	}
}

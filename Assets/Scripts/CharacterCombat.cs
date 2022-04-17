using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    // Player stats
    private float maxHealth;
    private float currentHealth;
    private float damage;
    private float attackSpeed;
    private float attackRange;

    // Misc fields
    public Transform attackPoint;
    public LayerMask playerLayer;
    private float nextAttackTime = 0.0f;

    private void Start()
	{
        maxHealth = 100.0f;
        currentHealth = maxHealth;
        damage = 20.0f;
        attackSpeed = 2.0f;
        attackRange = 0.5f;

        nextAttackTime = 0.0f;
    }

    private void FixedUpdate()
    {
        // Attacking conditions: 
        // 1) It must be the game state
        // 2) The player must have waited enough time since its last attack
        // 3) The "Attack" input for that character has been pressed
        if(GameManager.instance.GetCurrentMenuState() == MenuState.Game
            && Time.time >= nextAttackTime
            && GetComponent<Character2DController>().GetInputAction(Action.Attack).triggered)
                Attack();
    }

    /// <summary>
    /// Creates an attack for the player
    /// </summary>
    public void Attack()
	{
        // Get an array of all the enemies hit
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach(Collider2D hitEnemy in hitEnemies)
        {
            // Ensure the player isnt hitting itself
            if(hitEnemy.gameObject != gameObject)
            {
                Debug.Log(gameObject.name + " hits " + hitEnemy.gameObject.name + " for " + damage + " damage!");
                hitEnemy.GetComponent<CharacterCombat>().TakeDamage(damage);
			}
        }

        // Set when the player can attack again
        nextAttackTime = Time.time + 1.0f / attackSpeed;
	}

    /// <summary>
    /// Deals damage to the player
    /// </summary>
    /// <param name="amount">The amount of damage taken</param>
    public void TakeDamage(float amount)
	{
        currentHealth -= amount;

        // Check for death
        if(currentHealth <= 0)
		{
            Debug.Log(gameObject.name + " has died");
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
			GameManager.instance.ChangeMenuState(MenuState.PostGame);
        }
	}
}

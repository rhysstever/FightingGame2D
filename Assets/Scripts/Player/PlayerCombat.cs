using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask playerLayer;

    private int playerNum;
    private Character character;

    private float health = 20;
    private float damage = 5;
    private float attackRange = 0.5f;

    private float attackSpeed = 2.0f;
    private float attackTimerCurrent;

    private float specialSpeed = 2.0f;
    private float specialTimerCurrent;

    private bool isInvulnerable;
    private float invulnerableTimer = 0.1f;
    private float invulnerableTimerCurrent = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerNum = GetComponent<PlayerInfo>().GetPlayerNum;
        character = GetComponent<PlayerInfo>().GetPlayerCharacter;

        attackTimerCurrent = attackSpeed;
        isInvulnerable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Attack" + playerNum) 
            && attackTimerCurrent >= attackSpeed)
            Attack();

        if(Input.GetButtonDown("Special" + playerNum)
            && specialTimerCurrent >= specialSpeed)
            Special(character);
    }

	void FixedUpdate()
	{
        // Increment timers for both attacks
		attackTimerCurrent += Time.deltaTime;
        specialTimerCurrent += Time.deltaTime;

        // Increment the invulnerable timer if the player is invulnerable
        if(isInvulnerable)
            invulnerableTimerCurrent += Time.deltaTime;

        // If the player has been invulnerable long enough, make them vulnerable and reset the timer
        if(invulnerableTimerCurrent >= invulnerableTimer)
		{
            isInvulnerable = false;
            invulnerableTimerCurrent = 0;
		}
    }

    /// <summary>
    /// Makes an attack from the player
    /// </summary>
	private void Attack()
	{
        attackTimerCurrent = 0;
        // Search for any objects hit by the attack
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        // Loop through all hit objects and deal damage to them
        foreach(Collider2D hitObject in hitObjects)
		{
            // Only damage others, not self
            if(hitObject.gameObject != gameObject)
			{
                hitObject.gameObject.GetComponent<PlayerCombat>().TakeDamage(damage);
			}
        }
    }

    /// <summary>
    /// Deals damage to the player
    /// </summary>
    /// <param name="damage">The amount of damage being dealth</param>
    public void TakeDamage(float damage)
	{
        // If the player is vulnerable, deal damage to them and make them invulnerable
        if(!isInvulnerable)
        {
            isInvulnerable = true;
            health -= damage;
            Debug.Log(gameObject.name + " (" + character + ") has " + health + " health left");
        }

        // Check if the player has died
        if(health <= 0)
            gameObject.SetActive(false);
    }

    /// <summary>
    /// Use the special ability of the player
    /// </summary>
    /// <param name="character">The character the player is playing as</param>
    public void Special(Character character)
	{
        specialTimerCurrent = 0;

        // Determine the special used based on the player's character
        switch(character)
        {
            case Character.Con:
                break;
            case Character.Grace:
                break;
            case Character.Rhys:
                break;
            case Character.Sam:
                break;
        }
	}

    public void Buff()
	{

	}

    public void Debuff()
	{

	}
}

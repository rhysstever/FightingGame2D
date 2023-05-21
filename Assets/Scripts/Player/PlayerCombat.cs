using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask playerLayer;

    private int playerNum;
    private Character character;

    private float attackRange = 1.0f;
    private float attackRadius = 0.5f;
    private float attackTimerCurrent;
    private float specialSpeed = 2.0f;
    private float specialTimerCurrent;

    private bool isInvulnerable;
    private float invulnerableTimerTotal = 0.1f;
    private float invulnerableTimerCurrent = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerNum = GetComponent<PlayerInfo>().GetPlayerNum;
        character = PlayerManager.instance.GetPlayerInfoByPlayerNum(playerNum).GetPlayerCharacter;
        attackTimerCurrent = PlayerManager.instance.GetPlayerInfoByPlayerNum(playerNum).GetCurrentAttackSpeed;
        isInvulnerable = false;

        transform.GetChild(2).localPosition = new Vector2(attackRange, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(MenuManager.instance.CurrentMenuState == MenuState.Game)
        {
            if(Input.GetButtonDown("Attack" + playerNum)
                && attackTimerCurrent >= PlayerManager.instance.GetPlayerInfoByPlayerNum(playerNum).GetCurrentAttackSpeed)
			{
                GetComponent<Animator>().SetTrigger("Attack");
                Attack();
			}

            if(Input.GetButtonDown("Special" + playerNum)
                && specialTimerCurrent >= specialSpeed)
                Special(character);
        }
    }

	void FixedUpdate()
	{
        IncrementTimers();
    }

    /// <summary>
    /// Handles all timers associated with the player
    /// </summary>
    private void IncrementTimers()
	{
        // Increment both timers for both the attack and special ability
        attackTimerCurrent += Time.deltaTime;
        specialTimerCurrent += Time.deltaTime;

        // Invulnerable timer (only if the player is invulnerable)
        if(isInvulnerable)
		{
            // Increment
            invulnerableTimerCurrent += Time.deltaTime;

            // Disable invulnerability if the time is done
            if(invulnerableTimerCurrent >= invulnerableTimerTotal)
            {
                isInvulnerable = false;
                invulnerableTimerCurrent = 0;
            }
        }
    }

    /// <summary>
    /// Makes an attack from the player
    /// </summary>
	private void Attack()
	{
        attackTimerCurrent = 0;
        // Search for any objects hit by the attack
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);

        // Loop through all hit objects and deal damage to them
        foreach(Collider2D hitObject in hitObjects)
		{
            // Only damage others, not self
            if(hitObject.gameObject != gameObject)
			{
                hitObject.gameObject.GetComponent<PlayerCombat>().TakeDamage(PlayerManager.instance.GetPlayerInfoByPlayerNum(playerNum).GetCurrentDamage);
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
            float currentHealth = PlayerManager.instance.GetPlayerInfoByPlayerNum(playerNum).LoseHealth(damage);
            Debug.Log(gameObject.name + " (" + character + ") has " + currentHealth + " health left");
        }
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

    public void ApplyEffect(Effect effect)
	{
        PlayerManager.instance.GetPlayerInfoByPlayerNum(playerNum).AddEffect(effect);
	}
}

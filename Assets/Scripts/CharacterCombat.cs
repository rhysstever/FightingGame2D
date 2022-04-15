using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public float attackSpeed = 2.0f;
    private float nextAttackTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if(Input.GetAxis(
                CharacterManager.instance.GetPlayerSpecificInput(
                    InputType.Attack, 
                    GetComponent<Character2DController>().playerNum)) > 0.0f) 
            {
                Attack();
                nextAttackTime = Time.time + 1.0f / attackSpeed;
            }
        }
    }

    void Attack()
	{
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach(Collider2D hitEnemy in hitEnemies)
		{
            if(hitEnemy.gameObject != gameObject)
                Debug.Log(hitEnemy.name);
		}
	}
}

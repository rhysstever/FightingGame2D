using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
	Buff, 
	Debuff,
	Stun
}

public class Effect
{
	private string effectName;
	private EffectType effectType;
	private float effectTimeTotal;
	private float effectTimeCurrent;

	private float healthChange;
	private float moveSpeedChange;
	private float damageChange;
	private float attackSpeedChange;

	public string EffectName { get { return effectName; } }
	public EffectType EffectType { get { return effectType; } }
	public bool IsOver { get { return effectTimeCurrent >= effectTimeTotal; } }

	public float HealthEffect { get { return healthChange; } }
	public float MoveSpeedEffect { get { return moveSpeedChange; } }
	public float DamageEffect { get { return damageChange; } }
	public float AttackSpeedEffect { get { return attackSpeedChange; } }

	public Effect(string name, EffectType type, float duration, float healthChange, float moveSpeedChange, float damageChange, float attackSpeedChange)
	{
		effectName = name;
		effectType = type;
		effectTimeTotal = duration;
		effectTimeCurrent = 0.0f;

		this.healthChange = healthChange;
		this.moveSpeedChange = moveSpeedChange;
		this.damageChange = damageChange;
		this.attackSpeedChange = attackSpeedChange;
	}

	public void Tick(float amount)
	{
		if(amount > 0.0f)
			effectTimeCurrent += amount;
	}
}

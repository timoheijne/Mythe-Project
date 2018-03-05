﻿using UnityEngine;

/// Created By Timo Heijne
/// <summary>
/// Generic Health Script that we can put on anything we'd like
/// </summary>
public class Health : MonoBehaviour {
	[SerializeField] private float _startingHealth = 100;
	public float CurHealth { get; private set; }

	/// <summary>
	/// An Event that invokes when the Object that has this health script supposedly dies
	/// </summary>
	public delegate void OnDeath();
	public OnDeath onDeath;
	
	/// <summary>
	/// An Event that invokes when the object that has this health script gets sum damage
	/// </summary>
	/// <param name="damageAmount">The amount of damage is has taken</param>
	/// <param name="curHeath">The health is currently has (after the damage has substracted)</param>
	/// <param name="startingHealth">The health this object has started with</param>
	public delegate void OnDamage(float damageAmount, float curHeath, float startingHealth);
	public OnDamage onDamage;

	void Start() {
		CurHealth = _startingHealth;
	}

	/// <summary>
	/// A function that substracts health from the current health
	/// </summary>
	/// <param name="amount">The amount of damage it should take</param>
	public void TakeDamage(float amount = 1) {
		CurHealth -= amount;
		CheckDeath();

		onDamage?.Invoke(amount, CurHealth, _startingHealth);
	}
	
	
	/// <summary>
	/// Check whether this object is dead or not
	/// </summary>
	/// <returns>A boolean if true object is dead</returns>
	public bool IsDead() {
		return (CurHealth <= 0);
	}

	private void CheckDeath() {
		if (CurHealth <= 0)  onDeath?.Invoke();
	}
}

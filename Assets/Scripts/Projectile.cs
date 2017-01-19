using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public float damage = 100.0f;

	// Once it hits a projectile, destroy the game object
	public void Hit()
	{
		Destroy(gameObject);
	}

	// Return the damage this projectile deals. We'll use this on other classes so they know
	// how much damage is dealt
	public float GetDamage()
	{
		return damage;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	public float health = 150.0f;
	public GameObject projectile;
	public float projectileSpeed;

	// Frequency of fire
	public float shotsPerSecond = 0.5f;

	private void Update()
	{
		// 
		float probablility = Time.deltaTime * shotsPerSecond;

		if (Random.value < probablility)
		{
			Fire();
		}
	}

	void Fire()
	{
		Vector3 startPosition = this.transform.position + new Vector3(0.0f, -1.0f, 0.0f);
		GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -projectileSpeed, 0.0f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// We get the game object of the projectile
		Projectile missile = collision.gameObject.GetComponent<Projectile>();

		// If it exists, then log to console.
		if (missile)
		{
			Debug.Log("Hit by projectile");

			// Minus the health of this enemy by the amount of damage determined by the laser
			// from the Projectile class and destroy the laser. If health < 0, destroy it
			health -= missile.GetDamage();
			missile.Hit();

			if (health <= 0.0f)
			{
				Destroy(gameObject);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float padding;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 250.0f;

	private float xMin, xMax;

	// Use this for initialization
	void Start () {
		// Initialize the play space so we know the boundaries
		// We grab the edges of the camera's viewport and set that to our min/max values
		// We give a little padding instead of setting the anchor point of the sprite;
		float distanceToCamera = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		xMin = leftEdge.x + padding;
		xMax = rightEdge.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Using Time.deltaTime in movement is important so that the movement is independent of the framerate
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			//gameObject.transform.position += new Vector3(-speed * Time.deltaTime, 0.0f, 0.0f);
			// OR use 
			this.transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.RightArrow))
		{
			//gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
			// OR use
			this.transform.position += Vector3.right * speed * Time.deltaTime;
		}

		//Clamp so it makes sure the x value of our ship will never be out of bounds
		float newX = Mathf.Clamp(this.transform.position.x, xMin, xMax);
		this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			// We use InvokeRepeating so that we can hold down the space bar but not have it
			// instantiate a beam every update.
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			// Works together with InvokeRepeating. When space bar is not pressed anymore
			// Stop invoking the Fire method
			CancelInvoke("Fire");
		}
	}

	void Fire()
	{
		Vector3 offset = new Vector3(0.0f, 1.0f, 0.0f);
		GameObject beam = Instantiate(projectile, this.transform.position + offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, projectileSpeed, 0.0f);
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

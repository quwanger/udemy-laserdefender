using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If any gameobject collides with this box collider, destroy it
public class Shredder : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(collision.gameObject);
	}
}

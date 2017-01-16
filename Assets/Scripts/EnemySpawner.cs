using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10.0f;
	public float height = 5.0f;
	public float speed = 1.0f;

	private float xMin, xMax;
	private bool movingRight = true;

	// Use this for initialization
	void Start () {
		// Read this as every child transform under the EnemySpawner gameObject
		// This will set the position of the enemy prefabs we are instantaiting to the position of the Position gameObjects we created in the editor
		foreach(Transform child in transform)
		{
			// Instantiate an enemy at the origin and make sure it sits as a child and in the center of the Position gameObject
			GameObject enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity) as GameObject;
			// enemy.transform is the transform of the enemy prefab. Whereas child is the transform of Position gameObject
			// Used to make sure our hierarchy is clean
			enemy.transform.parent = child;
		}

		// Set the boundary of the play space for the enemy spawner
		float distanceToCamera = this.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		xMin = leftEdge.x;
		xMax = rightEdge.x;
	}
	
	// Update is called once per frame
	void Update () {
		// Increment the position of the spawner to the left or right
		if (movingRight)
		{
			this.transform.position += Vector3.right * speed * Time.deltaTime;
		} else
		{
			this.transform.position += Vector3.left * speed * Time.deltaTime;
		}

		// We need to add or subtract half the width of the spawner because the anchor is in the middle
		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);

		// If the left edge is less than the min, flip and vice versa
		if (leftEdgeOfFormation < xMin)
		{
			movingRight = true;
		} else if (rightEdgeOfFormation > xMax)
		{
			movingRight = false;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(this.transform.position, new Vector3(width, height, 0.0f));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10.0f;
	public float height = 5.0f;
	public float speed = 1.0f;
	public float spawnDelay = 0.5f;

	private float xMin, xMax;
	private bool movingRight = true;

	// Use this for initialization
	void Start () {
		SpawnUntilFull();

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

		if (AllMembersDead())
		{
			Debug.Log("Empty Formation");
			SpawnUntilFull();
		}
	}

	void SpawnEnemies()
	{
		// Read this as every child transform under the EnemySpawner gameObject
		// This will set the position of the enemy prefabs we are instantaiting to the position of the Position gameObjects we created in the editor
		foreach (Transform position in transform)
		{
			// Instantiate an enemy at the origin and make sure it sits as a child and in the center of the Position gameObject
			GameObject enemySpawner = Instantiate(enemyPrefab, position.position, Quaternion.identity) as GameObject;
			// enemy.transform is the transform of the enemy prefab. Whereas child is the transform of Position gameObject
			// Used to make sure our hierarchy is clean
			enemySpawner.transform.parent = position;
		}
	}

	void SpawnUntilFull()
	{
		// Store the next free position
		Transform freePosition = NextFreePosition();

		// And create a new enemy at that new free position if it exists
		if (freePosition)
		{
			GameObject enemySpawner = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemySpawner.transform.parent = freePosition;

			// Add a delay so they don't spawn immediately
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}

	Transform NextFreePosition()
	{
		foreach (Transform position in transform)
		{
			// If there is no enemy go under the position go, then return a position
			// This means there's a free position
			if (position.childCount == 0)
			{
				return position;
			}
		}

		// If it has an enemy go, then return null
		return null;
	}

	// Check if the transform of all the child object (i.e. if the Position gameobject is empty) 
	// are empty. If they are, return true
	bool AllMembersDead()
	{
		// Going over every child transform of the transform of the parent
		// in this case, the EnemySpawner
		foreach(Transform position in transform)
		{
			// Check if a child exists in position. If it does, then an enemey exists
			if (position.childCount > 0)
			{
				return false;
			}
		}

		return true;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(this.transform.position, new Vector3(width, height, 0.0f));
	}
}

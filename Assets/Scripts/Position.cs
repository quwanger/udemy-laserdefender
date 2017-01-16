using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This function draws gizmos on gameObjects with this class
// It allows us to see the position of this game object even when it's not selected
public class Position : MonoBehaviour {

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(this.transform.position, 1);
	}
}

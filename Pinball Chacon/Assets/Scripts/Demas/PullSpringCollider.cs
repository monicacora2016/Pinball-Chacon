using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullSpringCollider : MonoBehaviour {

	private PullSpringScript parentScript;

	// This script tells the PullSpringScript when the ball is colliding with the top of the spring
	// This is needed because you can't detect collision of child objects without adding a rigidbody to the parent

	void Awake () {
		// Get parent script
		parentScript = GetComponentInParent<PullSpringScript> ();
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ball") {
			//Debug.Log ("Ball enter!");
			parentScript.BallCollisionEnter ();
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Ball") {
			//Debug.Log ("Ball exit!");
			parentScript.BallCollisionExit ();
		}
	}
}

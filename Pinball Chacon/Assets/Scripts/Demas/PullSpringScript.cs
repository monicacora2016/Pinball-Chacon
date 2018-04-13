using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullSpringScript : MonoBehaviour {

	public string inputName = "DownInput";
	public float maxDistance = 2;
	public float speed = 1;
	public float power = 5000;

	private GameObject ball;
	private Rigidbody ballRB;
	private Transform springTop;
	private float springTopDefault;
	private Transform spring;

	private bool firing = false;
	public bool ballColliding = false;

	public bool active = true;

	void Awake() {
		ball = GameObject.Find("Ball");
		ballRB = ball.GetComponent<Rigidbody> ();
		springTop = gameObject.transform.GetChild (1);
		springTopDefault = springTop.position.z;
		spring = gameObject.transform.GetChild (0);
	}

	public void BallCollisionEnter() {
		// Activated by spring top in PullSpringCollider.cs
		ballColliding = true;
	}

	public void BallCollisionExit() {
		// Activated by spring top in PullSpringCollider.cs
		ballColliding = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton (inputName) && active) {
			// Move spring down
			if (springTop.position.z < springTopDefault + maxDistance) {
				springTop.Translate (0, speed * Time.deltaTime, 0);
				scaleSpring ();
				firing = true;
			}
		} else if (springTop.position.z > springTopDefault) {
			// Shoot spring
			if (firing && ballColliding) {
				ball.transform.TransformDirection (Vector3.forward * 20);
				ballRB.AddForce (0, 0, -(springTop.position.z-springTopDefault)/maxDistance * power);
				firing = false;
				ballColliding = false;
			}
			springTop.Translate (0, -10 * Time.deltaTime, 0);
			scaleSpring ();
		}

		if (springTop.position.z < springTopDefault) {
			// Make sure the spring can't move to far
			firing = false;
			springTop.position = new Vector3(springTop.position.x, springTop.position.y, springTopDefault);
			scaleSpring ();
		}
	}

	void scaleSpring () {
		spring.localScale = new Vector3 (100, 15+85*(1-(springTop.position.z-springTopDefault)/maxDistance), 100);
	}
}

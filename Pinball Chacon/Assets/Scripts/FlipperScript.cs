using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperScript : MonoBehaviour {

	public string inputName;
	public float defaultAngle = 0f;
	public float activeAngle = -75;
	public float flipperStrength = 10000f;
	public float flipperDamper = 100f;

	private HingeJoint hinge;

	void Awake () {
		hinge = GetComponent<HingeJoint>();
		hinge.useSpring = true;
	}

	// Update is called once per frame
	void Update () {
		JointSpring spring = new JointSpring ();
		spring.spring = flipperStrength;
		spring.damper = flipperDamper;

		if (Input.GetButton (inputName)) {
			spring.targetPosition = activeAngle;
		} else {
			spring.targetPosition = defaultAngle;
		}
		hinge.spring = spring;
		hinge.useLimits = true;
		JointLimits limits = hinge.limits;
		limits.min = defaultAngle;
		limits.max = activeAngle;
		hinge.limits = limits;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

	public GameObject messageReceiver;
	public string[] messages;
	public string[] value;

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Ball") {
			//Debug.Log ("Ball entered "+gameObject.name+"!");
			foreach (string m in messages) {
				messageReceiver.SendMessage (m, value);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Image[] hearts;
	public GameObject[] targetsTop = new GameObject[3];
	public RectTransform scoreBarMask;
	public Text scoreText;

	public bool inSpring = true;
	public int lives = 5;
	public int score = 0;

	void Awake() {
		Screen.SetResolution(414, 637, false );
		hearts = GameObject.Find ("Hearts").GetComponentsInChildren<Image>();
		GameObject[] temp = GameObject.FindGameObjectsWithTag ("TargetTop");
		for (int i = 0; i < temp.Length; i++) {
			int j = int.Parse(temp [i].name.Substring(9, 1));
			targetsTop [j] = temp [i];
		}
	}

	void NewRound () {
		lives--;
		if (lives > 0) {
			for (int i = 5; i > lives; i--) {
				// Remove a heart by playing the animation
				hearts [i - 1].GetComponentInParent<Animator>().SetTrigger("RemoveHeart");
			}
		} else {
			Debug.Log ("Game Over");
			for (int i = 0; i < 5; i++) {
				// Remove all hearts
				hearts [i].GetComponentInParent<Animator>().SetTrigger("RemoveHeart");
			}
			// Display GameOverText
			GameObject.Find ("GameOverText").GetComponent<Animator>().SetTrigger("ShowGameOverText");
			GameObject.Find ("springParent").GetComponent<PullSpringScript> ().active = false;
		}
	}

	public void AddToScore(int amount) {
		float previousHeight = 0.02f + 0.21f*Mathf.Floor(score/100);
		score += amount;
		scoreText.color = new Color (1.0f, 0.66667f*(score/1500f), 0.2f);
		StartCoroutine (scoreAnim (previousHeight));
	}
	IEnumerator scoreAnim(float h) {
		scoreBarMask.sizeDelta = new Vector2(1.12f, h+0.21f);
		yield return new WaitForSeconds(0.25f);
		scoreBarMask.sizeDelta = new Vector2(1.12f, h);
		yield return new WaitForSeconds(0.25f);
		scoreBarMask.sizeDelta = new Vector2(1.12f, h+0.21f);
		yield return new WaitForSeconds(0.25f);
		scoreBarMask.sizeDelta = new Vector2(1.12f, h);
		yield return new WaitForSeconds(0.25f);
		scoreBarMask.sizeDelta = new Vector2(1.12f, 0.02f + 0.21f*Mathf.Floor(score/100));
	}

	void TriggerTop(string[] param) {
		AddToScore (25);
		int i = int.Parse (param[0]);
		targetsTop[i].GetComponent<Animator>().SetBool("TargetOn", true);
		bool allOn = true;
		for (int a = 0; a < 3; a++) {
			if (targetsTop[a].GetComponent<Animator>().GetBool("TargetOn") == false)
				allOn = false;
		}
		if (allOn)
			StartCoroutine(targetsTopWin ());
	}
	IEnumerator targetsTopWin () {
		AddToScore (300);
		yield return new WaitForSeconds(0.4f);
		for (int a = 0; a < 3; a++) {
			targetsTop[a].GetComponent<Animator>().SetBool("TargetOn", false);
		}
	}


	void SpringTrigger (string[] param) {
		if (param[0] == "Enter") {
			inSpring = true;
		} else if (param[0] == "Leave") {
			inSpring = false;
		} 
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	[SerializeField] private Text timerDisplay;

	private float timer = 15.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (GameManager.instance.EnterGame && !GameManager.instance.GameOver) {

			timer -= Time.deltaTime;

			if (timer <= 0) {
				GameManager.instance.TimesUp ();
			} else {
				RunTimer ();
			}
		}

		if (GameManager.instance.GameOver) {
			timer = 15.0f;
		}
	}

	void RunTimer () {
		float seconds = Mathf.Floor(timer % 60);

		timerDisplay.text = "Time: " + seconds.ToString ();
	}
}

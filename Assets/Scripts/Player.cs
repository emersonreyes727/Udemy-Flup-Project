using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	private Animator anim;
	private Rigidbody rb;
	private AudioSource audioSource;

	private bool gravityOn = false;
	private float moveUpForce = 100.0f;
	private int score = 0;

	[SerializeField] private AudioClip sfxMove;
	[SerializeField] private AudioClip sfxDeath;
	[SerializeField] private Text scoreDisplay;

	void Awake () {
		Assert.IsNotNull (sfxMove);
		Assert.IsNotNull (sfxDeath);
		Assert.IsNotNull (scoreDisplay);
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.GameRestarted) {
			score = 0;

			scoreDisplay.text = "Score: " + score.ToString ();
		}
	}

	void FixedUpdate () {
		if (!GameManager.instance.GameOver && GameManager.instance.EnterGame) {
			// Do not add any Rigidbody codes here. It slow down the mouse clicks

			if (Input.GetMouseButtonDown(0)) {
				rb.useGravity = true;
				rb.detectCollisions = true;
				gravityOn = true;

				anim.Play ("Move");
				audioSource.PlayOneShot (sfxMove);

				GameManager.instance.RestartFalse ();
			}	
		}

		if (gravityOn == true) {
			gravityOn = false;

			rb.velocity = new Vector2(0, 0);

			// adds force so player can move up
			rb.AddForce (new Vector2 (0, moveUpForce), ForceMode.Impulse);
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (!GameManager.instance.PlayerWinGame) {
			if (collision.gameObject.tag == "obstacle") {

				// pushes the player flying to the side
				rb.AddForce (new Vector3 (-100.0f, 200.0f, 200.0f), ForceMode.Impulse);
				audioSource.PlayOneShot (sfxDeath);
				
				GameManager.instance.GameIsOver ();
			}
		}

		if (collision.gameObject.tag == "coin") {
			score += 1;

			collision.gameObject.SetActive(false);

			scoreDisplay.text = "Score: " + score.ToString ();

			if (score == 3) {
				GameManager.instance.PlayerWin ();

				// coins will not be detected once the score is 3
				rb.detectCollisions = false;

				// makes the player bigger
				transform.localScale += new Vector3 (7.0f, 7.0f, 7.0f);

				// moves the player to the center of the screen.
				transform.position  = new Vector3 (transform.localPosition.x + 8, 3.0f, transform.localPosition.z);

				// rotates the player so that its looking forward. Rotates the Y position only
				Quaternion rotatePlayer = new Quaternion(transform.localPosition.x, 182.0f, transform.localPosition.z, 0);
				transform.rotation = rotatePlayer;

				// player is unmovable
				rb.isKinematic = true;
			}
		}

	}
}



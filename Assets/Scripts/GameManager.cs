using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject restartGame;
	[SerializeField] private GameObject exitGame;

	[SerializeField] private Transform playerTransform;
	[SerializeField] private Rigidbody playerRb;

	[SerializeField] private Transform trackOneTransform;
	[SerializeField] private Transform trackTwoTransform;
	[SerializeField] private Transform trackThreeTransform;

	[SerializeField] private Transform groundOneTransform;
	[SerializeField] private Transform groundTwoTransform;
	[SerializeField] private Transform groundThreeTransform;
	[SerializeField] private Transform groundFourTransform;

	[SerializeField] private Transform coinOneTransform;
	[SerializeField] private Transform coinTwoTransform;
	[SerializeField] private Transform coinThreeTransform;
	[SerializeField] private Transform coinFourTransform;
	[SerializeField] private Transform coinFiveTransform;

	[SerializeField] private GameObject coinOneGameObject;
	[SerializeField] private GameObject coinTwoGameObject;
	[SerializeField] private GameObject coinThreeGameObject;
	[SerializeField] private GameObject coinFourGameObject;
	[SerializeField] private GameObject coinFiveGameObject;

	[SerializeField] private Rigidbody coinOneRb;
	[SerializeField] private Rigidbody coinTwoRb;
	[SerializeField] private Rigidbody coinThreeRb;
	[SerializeField] private Rigidbody coinFourRb;
	[SerializeField] private Rigidbody coinFiveRb;

	[SerializeField] private Text gameOverDisplay;
	[SerializeField] private Text playerWinDisplay;
	[SerializeField] private Text timesUpDisplay;
	[SerializeField] private Text scoreDisplay;

	// player has not click on the play button
	private bool enterGame = false;

	// the game is over
	private bool gameOver = false;

	// player win game
	private bool playerWinGame = false;

	// player restarts game
	private bool gameRestarted = false;

	public bool EnterGame {
		// a getter
		get { return enterGame; }
	}

	public bool GameOver {
		// a getter
		get { return gameOver; }
	}

	public bool PlayerWinGame {
		// a getter
		get { return playerWinGame; }
	}

	public bool GameRestarted {
		// a getter
		get { return gameRestarted; }
	}

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		Assert.IsNotNull (mainMenu);
		Assert.IsNotNull (restartGame);
		Assert.IsNotNull (exitGame);

		Assert.IsNotNull (playerTransform);
		Assert.IsNotNull (playerRb);

		Assert.IsNotNull (trackOneTransform);
		Assert.IsNotNull (trackTwoTransform);
		Assert.IsNotNull (trackThreeTransform);

		Assert.IsNotNull (groundOneTransform);
		Assert.IsNotNull (groundTwoTransform);
		Assert.IsNotNull (groundThreeTransform);
		Assert.IsNotNull (groundFourTransform);

		Assert.IsNotNull (coinOneTransform);
		Assert.IsNotNull (coinTwoTransform);
		Assert.IsNotNull (coinThreeTransform);
		Assert.IsNotNull (coinFourTransform);
		Assert.IsNotNull (coinFiveTransform);

		Assert.IsNotNull (coinOneGameObject);
		Assert.IsNotNull (coinTwoGameObject);
		Assert.IsNotNull (coinThreeGameObject);
		Assert.IsNotNull (coinFourGameObject);
		Assert.IsNotNull (coinFiveGameObject);

		Assert.IsNotNull (coinOneRb);
		Assert.IsNotNull (coinTwoRb);
		Assert.IsNotNull (coinThreeRb);
		Assert.IsNotNull (coinFourRb);
		Assert.IsNotNull (coinFiveRb);

		Assert.IsNotNull (gameOverDisplay);
		Assert.IsNotNull (playerWinDisplay);
		Assert.IsNotNull (timesUpDisplay);
		Assert.IsNotNull (scoreDisplay);
	}

	void Start () {
		// hides restart button
		restartGame.SetActive (false);
		exitGame.SetActive (false);
	}

	void Update () {
		// stops coins from spinning
		coinOneRb.freezeRotation = true;
		coinTwoRb.freezeRotation = true;
		coinThreeRb.freezeRotation = true;
		coinFourRb.freezeRotation = true;
		coinFiveRb.freezeRotation = true;
	}

	void FixedUpdate () {
		// stops the player from spinning after game restart
		playerRb.freezeRotation = true;

		// stops the coins from spinning after game restart
		coinOneRb.freezeRotation = true;
		coinTwoRb.freezeRotation = true;
		coinThreeRb.freezeRotation = true;
		coinFourRb.freezeRotation = true;
		coinFiveRb.freezeRotation = true;
	}

	public void PlayerEnteredGame () {
		mainMenu.SetActive (false);
		enterGame = true;

		scoreDisplay.text = "Score: ";
	}

	public void GameIsOver () {
		playerRb.freezeRotation = true;

		gameOverDisplay.text = "GAME OVER!";		
		gameOver = true;

		restartGame.SetActive (true);
		exitGame.SetActive (true);
	}

	public void PlayerWin () {
		// hides the ground
		groundOneTransform.position = new Vector3 (-200.00f, 7.10f, 24.49f);
		groundTwoTransform.position = new Vector3 (-203.00f, 4.27f, 24.49f);
		groundThreeTransform.position = new Vector3 (-206.00f, 9.47f, 24.49f);
		groundFourTransform.position = new Vector3 (-209.00f, 7.11f, 24.49f);

		playerWinDisplay.text = "YOU WIN!";
		playerWinGame = true;
		gameOver = true;

		restartGame.SetActive (true);
		exitGame.SetActive (true);
	}

	public void TimesUp () {
		timesUpDisplay.text = "TIMES UP!";
		gameOver = true;

		restartGame.SetActive (true);
		exitGame.SetActive (true);
	}

	public void Restart () {
		restartGame.SetActive (false);
		exitGame.SetActive (false);

		playerRb.isKinematic = false;
		playerRb.detectCollisions = true;

		// rotates the player back to its starting rotation
		Quaternion rotation = Quaternion.Euler (0, 80.0f, 0);
		playerTransform.rotation = rotation;

		// moves back player to start position
		playerTransform.position = new Vector3 (3.0f, 8.0f, 24.0f);
		playerTransform.localScale = new Vector3 (1.40f, 1.40f, 1.40f);

		// player won't spin
		playerRb.freezeRotation = true;

		// player would not be affected by AddForce
		playerRb.velocity = Vector3.zero;

		// moves back track to start position
		trackOneTransform.position = new Vector3 (-3.50f, -1.0f, 0);
		trackTwoTransform.position = new Vector3 (27.40f, -1.0f, 0);
		trackThreeTransform.position = new Vector3 (58.40f, -1.0f, 0);

		// moves back ground to start position
		groundOneTransform.position = new Vector3 (23.00f, 7.10f, 24.49f);
		groundTwoTransform.position = new Vector3 (29.00f, 4.27f, 24.49f);
		groundThreeTransform.position = new Vector3 (35.00f, 9.47f, 24.49f);
		groundFourTransform.position = new Vector3 (41.00f, 7.11f, 24.49f);

		// moves back coin to start position
		coinOneTransform.position = new Vector3 (26.00f, 10.07f, 24.54f);
		coinTwoTransform.position = new Vector3 (32.00f, 5.84f, 24.54f);
		coinThreeTransform.position = new Vector3 (38.00f, 12.01f, 24.54f);
		coinFourTransform.position = new Vector3 (44.00f, 6.31f, 24.54f);
		coinFiveTransform.position = new Vector3 (50.00f, 9.58f, 24.54f);

		// reactivate coins
		coinOneGameObject.SetActive (true);
		coinTwoGameObject.SetActive (true);
		coinThreeGameObject.SetActive (true);
		coinFourGameObject.SetActive (true);
		coinFiveGameObject.SetActive (true);

		// stops coins from spinning
		coinOneRb.velocity = Vector3.zero;
		coinTwoRb.velocity = Vector3.zero;
		coinThreeRb.velocity = Vector3.zero;
		coinFourRb.velocity = Vector3.zero;
		coinFiveRb.velocity = Vector3.zero;

		// reset to original values
		gameOver = false;
		playerWinGame = false;
		gameRestarted = true;

		// clears displays
		timesUpDisplay.text = "";
		playerWinDisplay.text = "";
		gameOverDisplay.text = "";
	}

	public void RestartFalse () {
		gameRestarted = false;
	}

	public void ExitTheGame () {
		Application.Quit ();
	}
}

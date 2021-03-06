﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class GM : MonoBehaviour {

	// Track power ups with global booleans inside this Singleton class.
	public bool godMode=false;
	public static bool heavyBall=false;		/* powerUpIndex[1] from PowerUp.cs. Ball will crash through bricks without collision, only trigger */
	public static bool groundImmune=false;	/* powerUpIndex[2] from PowerUp.cs. Ball will bounce off the ground instead of killing the paddle */
	public static bool sloMo=false;			/* powerUpIndex[3] from PowerUp.cs. Ball/World will move in SloMo, while player paddle will move outside of gametime. */

	public int lives = 3;					/* Player Lives */
	public int bricks = 55;					/* Bricks needed to win level. This is not nearly sophisticated enough yet. */
	public float resetDelay = 1f;			/* Time to wait before resetting after death. */
	public Text livesText;					/* UI Object that holds player lives on scene canvas. */
	public Text velText;
	public GameObject INVText;
	public GameObject SLOText;
	public GameObject WGTText;
	public GameObject gameOver;				/* UI Object that holds GAME OVER text on scene canvas */
	public GameObject levelComplete;		/* UI Object that holds Level Complete text on scene canvas */
	public GameObject bricksPrefab;			/* Object that holds the 55 level bricks as a parent. */
	public GameObject paddle;				/* Object that holds paddle/ball, and then paddle when ball is no longer child. */
	public GameObject deathParticles;		/* Particle effect when player dies */
	public AudioClip playerDeath;			/* Explosion audio clip for player death. */
	public AudioClip poweredUp;				/* Player picked up a power up */
	AudioSource gameAudio;					/* Audiosource for GameManager originating sounds. */
	public AudioSource ambientAudio;
	public static GM instance = null;

	private GameObject clonePaddle;			/* Object to hold clones of the player paddle prefab */


	// Poor mans <Singleton>
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		Cursor.visible = false;
		if (godMode) {
			groundImmune = true;
		}
		Setup();

	}

	public void Setup()
	{
		gameAudio = GetComponent<AudioSource> ();
		SetupPaddle ();						// Setup the player
		Instantiate(bricksPrefab, bricksPrefab.transform.position, Quaternion.identity); // Setup the bricks
	}

	void CheckGameOver()
	{
		if (bricks < 1) {
			levelComplete.SetActive(true);
			Time.timeScale = .25f; // Slomo effect after win
			Invoke ("Reset", resetDelay);
		}

		if (lives < 1)
		{
			gameOver.SetActive(true);
			Time.timeScale = .25f; // Slomo effect after Game Over
			Invoke ("Reset", resetDelay);
		}

	}

	void Reset()
	{
		Time.timeScale = 1f;
		heavyBall = false;
		sloMo = false;
		groundImmune = false;
		SceneManager.LoadScene ("levelone");
	}

	public void LoseLife()
	{
		gameAudio.PlayOneShot (playerDeath, 0.7f);
		lives--;
		livesText.text = "Lives: " + lives;
		Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
		Destroy(clonePaddle);
		Destroy(GameObject.FindWithTag("Ball"));
		Invoke ("SetupPaddle", resetDelay);
		CheckGameOver();
	}

	void SetupPaddle()
	{
		clonePaddle = Instantiate(paddle, transform.position, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
	}

	public void DestroyBrick()
	{
		bricks--;
		CheckGameOver();
	}

	public void LoadPowerUp(int powerUpIndex){
		Debug.Log (powerUpIndex);
		gameAudio.PlayOneShot (poweredUp, 0.7f);
		if (powerUpIndex == 0) {
			heavyBall = true;
			WGTText.SetActive (true);
			CancelInvoke ("UnloadPowerUp1");
			Invoke ("UnloadPowerUp1", 15.0f);
		}
		if (powerUpIndex == 1) {
			groundImmune = true;
			INVText.SetActive (true);
			CancelInvoke ("UnloadPowerUp2");
			Invoke ("UnloadPowerUp2", 15.0f);
		}
		if (powerUpIndex == 2) {
			sloMo = true;
			Time.timeScale = 0.25f;
			SLOText.SetActive (true);
			GameObject.FindObjectOfType<AudioSource> ().pitch = 0.5f;
			ambientAudio.pitch = 0.5f;
			CancelInvoke ("UnloadPowerUp3");
			Invoke ("UnloadPowerUp3", 4.0f);
		}
	}

	public void UnloadPowerUp1(){
		heavyBall = false;
		WGTText.SetActive (false);
	}
	public void UnloadPowerUp2(){
		if (!godMode) {
			groundImmune = false;
			INVText.SetActive (false);
		}
	}
	public void UnloadPowerUp3(){
		sloMo = false;
		SLOText.SetActive (false);
		Time.timeScale = 1f;
		GameObject.FindObjectOfType<AudioSource> ().pitch = 1f;
		ambientAudio.pitch = 1f;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	public int lives = 3;
	public int bricks = 45;
	public float resetDelay = 1f;
	public Text livesText;
	public GameObject gameOver;
	public GameObject levelComplete;
	public GameObject bricksPrefab;
	public GameObject paddle;
	public GameObject deathParticles;
	public AudioClip playerDeath;
	AudioSource gameAudio;
	public static GM instance = null;

	private GameObject clonePaddle;


	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		Setup();

	}

	public void Setup()
	{
		gameAudio = GetComponent<AudioSource> ();
		SetupPaddle ();
		Instantiate(bricksPrefab, transform.position, Quaternion.identity);
	}

	void CheckGameOver()
	{
		if (bricks < 1) {
			levelComplete.SetActive(true);
			Time.timeScale = .25f;
			Invoke ("Reset", resetDelay);
		}

		if (lives < 1)
		{
			gameOver.SetActive(true);
			Time.timeScale = .25f;
			Invoke ("Reset", resetDelay);
		}

	}

	void Reset()
	{
		Time.timeScale = 1f;
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
		if (powerUpIndex == 1) {
			
		}
	}
}

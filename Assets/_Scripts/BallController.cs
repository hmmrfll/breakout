using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour {
	public float ballInitialVelocity = 1200f;
	public float maxVelocity = 72f;
	private Rigidbody rb;
	private Vector3 ballPos;
	private bool ballInPlay;

	public AudioClip woodbSound;
	public AudioClip woodwSound;
	public AudioClip paddleSound;
	AudioSource brickAudio;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
		brickAudio = GetComponent<AudioSource> ();
		//Debug Velocity
		/*
		GameObject tempTextBox = (GameObject)Instantiate (velocityText);
		velMesh = tempTextBox.transform.GetComponent<TextMesh> ();
		velMesh.text = "TESTING"; */
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && ballInPlay == false) 
		{
			transform.parent = null;
			ballInPlay = true;
			rb.isKinematic = false;
			rb.AddForce (new Vector3 (0f, ballInitialVelocity, 0f));

		}
	}

	void OnCollisionEnter (Collision other){
		
		float cVolume = Mathf.Clamp(rb.velocity.magnitude/maxVelocity,0.2f,1.0f);
		if (other.gameObject.tag == "WoodBrick") {
			brickAudio.enabled = true;
			brickAudio.PlayOneShot (woodbSound, cVolume);
		}
		if (other.gameObject.tag == "WoodWall") {
			brickAudio.enabled = true;
			brickAudio.PlayOneShot (woodwSound, cVolume);
		}
		if (other.gameObject.tag == "Player") {
			brickAudio.enabled = true;
			brickAudio.PlayOneShot (paddleSound, cVolume);
		}
	}

	void FixedUpdate ()
	{
		if (ballInPlay == true) {
			float yPos = transform.position.y;
			float xPos = transform.position.x;
			ballPos = new Vector3 (xPos, yPos, 0f);
			transform.position = ballPos;
			rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxVelocity);
			// velMesh.text = "Normalized:" + rb.velocity.normalized; 
		}
	}
}


using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {
	public float paddleSpeed = 2f;
	public float paddleYPos = -24f;

	private Rigidbody rb;

	private Vector3 playerPos = new Vector3 (0, -24f, 0);

	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		float xPos = transform.position.x + ((Input.GetAxis ("Horizontal") + Input.GetAxis("Mouse X")) * paddleSpeed); // Take input from Keyboard, Joystick or Mouse
		playerPos = new Vector3 (Mathf.Clamp (xPos, -25f, 25f), paddleYPos, 0f);  // Clamp the player between the level boundaries
		rb.MovePosition (playerPos);  // Move the paddle
	}
}

using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {
	public float paddleSpeed = 2f;
	private Rigidbody rb;

	private Vector3 playerPos = new Vector3 (0, -15.5f, 0);

	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		float xPos = transform.position.x + (Input.GetAxis ("Horizontal") * paddleSpeed);
		playerPos = new Vector3 (Mathf.Clamp (xPos, -25f, 25f), -15.5f, 0f);
		rb.MovePosition (playerPos);
		//transform.position = playerPos;
	
	}
}

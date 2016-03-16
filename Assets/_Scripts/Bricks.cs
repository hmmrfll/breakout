using UnityEngine;
using System;
using System.Collections;

public class Bricks : MonoBehaviour {
	public GameObject brickParticle;
	public GameObject powerUp;
	public int powerUpChance = 100;
	public float floatStrength = 1;
	private int powerUpNumber = UnityEngine.Random.Range (0, 101);
	Vector3 floatY;
	float originalY;

	void Start(){
		this.originalY = this.transform.position.y;

	}

	void Awake(){
	}
	void OnCollisionEnter (Collision other)
	{
		Instantiate (brickParticle, transform.position, Quaternion.identity);
		if (powerUpNumber < powerUpChance) {
			Instantiate (powerUp, transform.position, Quaternion.Euler (0f, 0f, 270f));
		}
		GM.instance.DestroyBrick ();
		Destroy (gameObject);
	}

	void Update(){
		transform.position = new Vector3 (transform.position.x, originalY + ((float)Math.Sin (Time.time) * floatStrength), transform.position.z);
		
	}
}

using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Bricks : MonoBehaviour {
	public GameObject brickParticle;
	public float floatStrength = 1;
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
		GM.instance.DestroyBrick ();
		Destroy (gameObject);
	}

	void Update(){
		transform.position = new Vector3 (transform.position.x, originalY + ((float)Math.Sin (Time.time) * floatStrength), transform.position.z);
		
	}
}

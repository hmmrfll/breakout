using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Ball" && !GM.groundImmune) {
			GM.instance.LoseLife ();
		}
		if (other.gameObject.tag == "PWR") {
			Destroy (other.gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		// If ball hits ground, die
		if (other.gameObject.tag == "Ball" && !GM.groundImmune) {
			GM.instance.LoseLife ();
		}

		// Destroy power ups that fall on the ground
		if (other.gameObject.tag == "PWR") {
			Destroy (other.gameObject);
		}
	}
}

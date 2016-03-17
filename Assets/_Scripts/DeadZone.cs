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
		if (other.gameObject.tag == "Ball" && !GM.groundImmune) {
			GM.instance.LoseLife ();
		}
		if (other.gameObject.tag == "PWR") {
			Destroy (other.gameObject);
		}
	}
}

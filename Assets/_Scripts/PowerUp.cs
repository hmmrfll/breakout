using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	private string[] powerUpType = new string[3] {"WGT", "INV", "SLO"} ;
	private int powerUpIndex;
	private TextMesh powerUpText;

	// Use this for initialization
	void Awake () {
		
		powerUpIndex = Random.Range (1, 3);	
		powerUpText = GetComponentInChildren<TextMesh> ();
		powerUpText.text = powerUpType [powerUpIndex];

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag == "Player") {
			GM.instance.LoadPowerUp (powerUpIndex);
			Destroy (gameObject); 
		}
	}
}

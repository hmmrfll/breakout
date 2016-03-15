using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour {
	public float destroyTime = 10f;
	// Use this for initialization
	void Start () {

		Destroy (gameObject, destroyTime);
	
	}

}

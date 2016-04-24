using UnityEngine;
using System.Collections;

public class BlueButton : MonoBehaviour {

	bool enabled = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" && enabled) {
			enabled = false;
			Timer timer = GameObject.FindObjectOfType<Timer> ();
			timer.gameTimer = 1.0f;
		}
	}
}

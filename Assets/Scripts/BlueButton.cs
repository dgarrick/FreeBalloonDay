using UnityEngine;
using System.Collections;

public class BlueButton : MonoBehaviour {

	bool canBeUsed = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		if (!canBeUsed && gameObject.transform.localScale.y >= 0)
			gameObject.transform.localScale -= new Vector3 (0, Time.deltaTime*10, 0);
	}
		
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" && canBeUsed) {
			canBeUsed = false;
			Timer timer = GameObject.FindObjectOfType<Timer> ();
			timer.gameTimer = 1.0f;

		}
	}
}

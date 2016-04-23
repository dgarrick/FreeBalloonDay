using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float gameTimer;
    private GameObject timerText;

	// Use this for initialization
	void Start () {
        gameTimer = 60.0f;
        timerText = GameObject.Find("TimerText");
	}
	
    void FixedUpdate () {
        gameTimer -= Time.deltaTime;
        timerText.GetComponent<Text>().text = Mathf.Floor(gameTimer).ToString();
        Debug.Log(gameTimer.ToString());
    }

	// Update is called once per frame
	void Update () {
	
	}
}

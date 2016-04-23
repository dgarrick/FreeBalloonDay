using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float gameTimer;
    private GameObject timerText;

	// Use this for initialization
	void Start () {
        gameTimer = 30.0f;
        timerText = GameObject.Find("TimerText");
	}
	
    void FixedUpdate () {
        gameTimer -= Time.deltaTime;
        if (gameTimer < 0.0f)
        {
            gameTimer = 0.0f;
        }

        timerText.GetComponent<Text>().text = Mathf.Floor(gameTimer).ToString();

        if (gameTimer == 0.0f)
        {
            Debug.Log("Time is up, drop the spikes.");
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

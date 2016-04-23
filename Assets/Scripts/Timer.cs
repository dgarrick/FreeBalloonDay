using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float gameTimer;
    private GameObject timerText;
    bool spikesDropped;

	// Use this for initialization
	void Start () {
        spikesDropped = false;
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
            if (!spikesDropped)
            {
                spikesDropped = true;
                Debug.Log("Time is up, drop the spikes.");
                Spikes spikes = GameObject.FindObjectOfType<Spikes>();
                spikes.dropSpikes();
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

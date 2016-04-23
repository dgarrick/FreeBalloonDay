using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float gameTimer;
    private GameObject timerText;
    Text theText;
    bool spikesDropped;
    public int balloonsDestroyed = 0;
    public int reportingSpikes = 0;
    private int expectedSpikes = 0;

	// Use this for initialization
	void Start () {
        spikesDropped = false;
        timerText = GameObject.Find("TimerText");
        theText = timerText.GetComponent<Text>();
	}
	
    void FixedUpdate () {
        gameTimer -= Time.deltaTime;
        if (gameTimer < 0.0f)
        {
            gameTimer = 0.0f;
        }

        if(!spikesDropped)
            timerText.GetComponent<Text>().text = Mathf.Floor(gameTimer).ToString();
        else if (spikesDropped && reportingSpikes != expectedSpikes)
        {
            theText.fontSize = 1;
            theText.text = "Balloons\nDestroyed\n" + balloonsDestroyed;
        }
        else if (reportingSpikes == expectedSpikes)
        {
            if (balloonsDestroyed == 0)
            {
                theText.text = "You won!";
            }
            else
            {
                theText.text = "You lost";
            }
        }

        if (gameTimer == 0.0f)
        {
            if (!spikesDropped)
            {
                spikesDropped = true;
                Debug.Log("Time is up, drop the spikes.");
                Spikes spikes = GameObject.FindObjectOfType<Spikes>();
                expectedSpikes = spikes.dropSpikes();
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float gameTimer;
    private GameObject timerText;
    private float endWaitTimer = 1.5f;
    private float nextLevelTimer = 3f;
    Text theText;
    bool spikesDropped;
    public int balloonsDestroyed = 0;
    public int reportingSpikes = 0;
    private int expectedSpikes = 0;
    LevelTracker tracker;

	// Use this for initialization
	void Start () {
        spikesDropped = false;
        expectedSpikes = (GameObject.FindGameObjectsWithTag("Spike")).Length;
        timerText = GameObject.Find("TimerText");
        theText = timerText.GetComponent<Text>();
        tracker = GameObject.FindObjectOfType<LevelTracker>();
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
            endWaitTimer -= Time.deltaTime;
            if (balloonsDestroyed == 0)
            {
                if (endWaitTimer > 0.0f)
                    theText.text = "You won!";
                else
                {
                    nextLevelTimer -= Time.deltaTime;
                    if (nextLevelTimer <= 0)
                    {
                        DontDestroyOnLoad(tracker);
                        tracker.completedLevel();
                        Application.LoadLevel(tracker.getLevelName());
                    }
                    theText.text = "Next level\nin " + ((nextLevelTimer > 0.0f) ? Mathf.Floor(nextLevelTimer) : 0);
                }
            }
            else
            {
                if (endWaitTimer > 0.0f)
                    theText.text = "You lost";
                else
                {
                    nextLevelTimer -= Time.deltaTime;
                    if (nextLevelTimer <= 0)
                    {
                        DontDestroyOnLoad(tracker);
                        Application.LoadLevel(tracker.getLevelName());
                    }
                    theText.text = "Retry\nin " + ((nextLevelTimer > 0.0f) ? Mathf.Floor(nextLevelTimer) : 0);
                }
            }
        }

        if(gameTimer >= 0 && gameTimer <= 8.0f && !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }

        {

        }
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

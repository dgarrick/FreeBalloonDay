using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    public float gameTimer;
    private GameObject timerText;
    private float endWaitTimer = 1.5f;
    private float nextLevelTimer = 3f;
    Text theText;
    bool spikesDropped;
    public int balloonsDestroyed = 0;
    public int reportingSpikes = 0;
	public int evilBalloonsKilled = 0;
	private int totalEvilBalloons = 0;
    private int expectedSpikes = 0;
    LevelTracker tracker;

    // Use this for initialization
    void Start () {
        spikesDropped = false;
        expectedSpikes = (GameObject.FindGameObjectsWithTag("Spike")).Length;
		totalEvilBalloons = (GameObject.FindGameObjectsWithTag("EvilBalloon")).Length;
        timerText = GameObject.Find("TimerText");
        theText = timerText.GetComponent<Text>();
        tracker = GameObject.FindObjectOfType<LevelTracker>();
    }
	
    void FixedUpdate () {
        gameTimer -= Time.deltaTime;
		gameTimer = (gameTimer < 0.0f) ? 0.0f : gameTimer;
		if (!spikesDropped)
	    	timerText.GetComponent<Text> ().text = Mathf.Floor (gameTimer).ToString ();
		else {
	    	theText.fontSize = 1;
	    	theText.text = "Balloons\nDestroyed\n" + balloonsDestroyed;
			Debug.Log (totalEvilBalloons);
			Debug.Log (evilBalloonsKilled);
			if (reportingSpikes == expectedSpikes && evilBalloonsKilled == totalEvilBalloons)  {
	        	endWaitTimer = (endWaitTimer <= 0.0f) ? 0.0f : endWaitTimer - Time.deltaTime;
                if (endWaitTimer <= 0.0f) {
		    		nextLevelTimer = (nextLevelTimer <= 0.0f) ? 0.0f : nextLevelTimer - Time.deltaTime;
		    		if (balloonsDestroyed == 0)
		        		theText.text = "Next level\nin " + ((nextLevelTimer > 0.0f) ? Mathf.Floor(nextLevelTimer) : 0);
		    		else if (balloonsDestroyed >= 1)
		       			theText.text = "Retry in\nin " + ((nextLevelTimer > 0.0f) ? Mathf.Floor(nextLevelTimer) : 0);
		    		if (nextLevelTimer <= 0.0f) {
						if (balloonsDestroyed == 0) {
							DontDestroyOnLoad (tracker);
							tracker.completedLevel ();
						}
						else if (tracker.currLevel != 0)
							DontDestroyOnLoad (tracker);
						SceneManager.LoadScene(tracker.getLevelName());
		    		}
				}
            }
        }

        if(gameTimer >= 0 && gameTimer <= 8.0f && !gameObject.GetComponent<AudioSource>().isPlaying)
            gameObject.GetComponent<AudioSource>().Play();
        if (gameTimer == 0.0f)
        {
            if (!spikesDropped)
            {
                spikesDropped = true;
                Spikes spikes = GameObject.FindObjectOfType<Spikes>();
                spikes.dropSpikes();
            }
        }
    }

    // Update is called once per frame
    void Update () {
    
    }
}

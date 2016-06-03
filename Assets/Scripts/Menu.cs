using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

	LevelTracker levelTracker;
	AudioSource music;
	Dropdown levelSelecter;
	// Use this for initialization
	void Start () {
		// If we came from an ESC press, we already have one
		levelTracker = GameObject.FindObjectOfType<LevelTracker> ();
		if (levelTracker == null)
			createLevelTracker ();
		levelSelecter = GameObject.FindObjectOfType<Dropdown> ();
		if (levelSelecter == null)
			Debug.LogError ("No LevelSelecter (Dropdown) found in scene!");
		levelSelecter.ClearOptions ();
		levelSelecter.AddOptions (new List<string>(levelTracker.levels));
		// Set volume slider value
		music = levelTracker.gameObject.GetComponent<AudioSource>();
		GameObject.Find ("VolumeSlider").GetComponent<Slider> ().value = music.volume;
	}

	private void createLevelTracker() {
		GameObject LevelTrackerPrefab = Resources.Load ("Prefabs/LevelTracker") as GameObject;
		GameObject LevelTrackerObj = GameObject.Instantiate (LevelTrackerPrefab);
		levelTracker = GameObject.FindObjectOfType<LevelTracker> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setMusicVolume(float volume) {
		music.volume = volume;
	}

	public void startNew() {
		levelTracker.currLevel = 0;
		DontDestroyOnLoad (levelTracker);
		SceneManager.LoadScene (levelTracker.getLevelName());
	}

	public void loadLevel() {
		string toLoad = levelSelecter.options[levelSelecter.value].text;
		DontDestroyOnLoad(levelTracker);
		levelTracker.setLevelByName (toLoad);
		SceneManager.LoadScene (levelTracker.getLevelName());
	}

	public void exit() {
		Application.Quit ();
	}
}

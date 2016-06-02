using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

	LevelTracker levelTracker;
	Dropdown levelSelecter;
	// Use this for initialization
	void Start () {
		levelTracker = GameObject.FindObjectOfType<LevelTracker> ();
		if (levelTracker == null)
			Debug.LogError ("No LevelTracker found in scene!");
		levelSelecter = GameObject.FindObjectOfType<Dropdown> ();
		if (levelSelecter == null)
			Debug.LogError ("No LevelSelecter (Dropdown) found in scene!");
		levelSelecter.ClearOptions ();
		levelSelecter.AddOptions (new List<string>(levelTracker.levels));
	}
	
	// Update is called once per frame
	void Update () {
	
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

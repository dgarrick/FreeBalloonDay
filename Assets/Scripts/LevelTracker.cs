using UnityEngine;
using System.Collections;

public class LevelTracker : MonoBehaviour {

    public string[] levels;
    public int currLevel;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void completedLevel()
    {
        currLevel += 1;
    }

    public string getLevelName()
    {
        return levels[currLevel];
    }
}

using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

    Timer timer;
	// Use this for initialization
	void Start () {
        timer = GameObject.FindObjectOfType<Timer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void checkPierce()
    {
        GameObject[] balloons = GameObject.FindGameObjectsWithTag("Balloon");
        foreach (GameObject balloon in balloons)
        {
            if (Vector3.Distance(balloon.transform.position, gameObject.transform.position) < 2)
            {
                timer.balloonsDestroyed += 1;
            }
        }
        timer.reportingSpikes += 1;
    }

    public IEnumerator changePosition(Vector3 newPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        Vector3 oldPos = transform.position;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            gameObject.transform.position = Vector3.Lerp(oldPos, newPos, i);
            yield return null;
        }
        checkPierce();
    }
}

using UnityEngine;
using System.Collections;

public class Medal : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(changePosition(new Vector3(0.0f, -0.1f, 0.0f), 3.0f));
        StartCoroutine(changeRotation(Quaternion.Euler(0, -15, 0), 2.0f));
    }
	
    void FixedUpdate()
    {

    }

	// Update is called once per frame
	void Update () {
	
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
        StartCoroutine(changePosition(oldPos, time));
    }

    public IEnumerator changeRotation(Quaternion newPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        Quaternion oldPos = transform.rotation;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            gameObject.transform.rotation = Quaternion.Lerp(oldPos, newPos, i);
            yield return null;
        }
        StartCoroutine(changeRotation(oldPos, time));
    }
}

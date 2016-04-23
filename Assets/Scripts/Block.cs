using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 myPos = gameObject.transform.position;
            Vector3 playerPos = other.gameObject.transform.position;
            Vector3 diff = playerPos - myPos;
            bool xLarger = Mathf.Abs(diff.x) > Mathf.Abs(diff.z);
            if (xLarger && diff.x > 0)
                 StartCoroutine(changePosition(new Vector3(-4, 0, 0) + gameObject.transform.position));
            else if (xLarger && diff.x < 0)
                 StartCoroutine(changePosition(new Vector3(4, 0, 0) + gameObject.transform.position));
            else if (!xLarger && diff.z > 0)
                 StartCoroutine(changePosition(new Vector3(0, 0, -4) + gameObject.transform.position));
            else if (!xLarger && diff.z < 0)
                StartCoroutine(changePosition(new Vector3(0, 0, 4) + gameObject.transform.position));
            else
                Debug.Log("Something very bad happened");

        }
    }

    private IEnumerator changePosition(Vector3 newPos) {
        BoxCollider[] colliders = GetComponents<BoxCollider>();
        foreach (BoxCollider coll in colliders)
        {
            coll.enabled = false;
        }
        float time = 0.5f;  
        float i = 0.0f;
        float rate = 1.0f/time;
        Vector3 oldPos = transform.position;
        while (i < 1.0f) {
            i += Time.deltaTime * rate;
            gameObject.transform.position = Vector3.Lerp(oldPos, newPos, i);
            yield return null;
        }
        foreach (BoxCollider coll in colliders)
        {
            coll.enabled = true;
        }
    }
}

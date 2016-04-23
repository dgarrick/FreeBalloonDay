using UnityEngine;
using System.Collections;
using System;

public class ButtonScript : MonoBehaviour {
    float pressTime = 0f;
    Boolean enables = true;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && enabled)
        {
            GameObject[] spikes = GameObject.FindGameObjectsWithTag("Spike");
            Debug.Log("Player hit the button!");
            foreach (GameObject go in spikes)
            {
                Debug.Log("wow!");
                Color c1 = Color.red;
                Color c2 = Color.red;
                int lengthOfLineRenderer = 120;
                LineRenderer lineRenderer = go.GetComponent<LineRenderer>();
                if (!go.GetComponent<LineRenderer>())
                     lineRenderer = go.AddComponent<LineRenderer>();
                lineRenderer.enabled = true;
                lineRenderer.SetColors(c1, c2);
                lineRenderer.material = Resources.Load("Materials/Laser") as Material;
                lineRenderer.SetWidth(1F, 1F);
                lineRenderer.SetVertexCount(lengthOfLineRenderer);
                pressTime = Time.time;
                int i = 0;
                float x = go.transform.position.x;
                float y = 60 + go.transform.position.y;
                float z = go.transform.position.z;
                while (i < lengthOfLineRenderer)
                {
                    //Debug.Log("x " + x + " y " + (y-i) + " z " + z);
                    Vector3 pos = new Vector3(x, y - i, z);
                    lineRenderer.SetPosition(i, pos);
                    i++;
                }
            }
            enabled = false;
            StartCoroutine(waitAndKillLasers(5));
        }
    }

    IEnumerator waitAndKillLasers(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject[] spikes = GameObject.FindGameObjectsWithTag("Spike");
            Debug.Log("Time's up!");
            foreach (GameObject go in spikes)
            {
                LineRenderer lineRenderer = go.GetComponent<LineRenderer>();
                lineRenderer.enabled = false;
            }
    }
}
using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

    PlayerMovement player;
    GameObject endCameraPrefab;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        endCameraPrefab = Resources.Load("Prefabs/EndCamera") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void dropSpikes()
    {
        if (!player)
            player = GameObject.FindObjectOfType<PlayerMovement>();
        Destroy(player.gameObject);
        // Set camera somewhere convenient
        GameObject.Instantiate(endCameraPrefab);

        //Drop the spikes!
        Spike[] spikes = GameObject.FindObjectsOfType<Spike>();
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (Spike spike in spikes)
        {
            // Figure out if they collide or not
            bool matchingBlock = false;
            foreach (GameObject block in blocks)
            {
                if (block.transform.position.x == spike.transform.position.x && block.transform.position.z == spike.transform.position.z)
                {
                    matchingBlock = true;
                }
            }
            if (matchingBlock)
            {
                StartCoroutine(spike.changePosition(new Vector3(spike.gameObject.transform.position.x, 35, spike.gameObject.transform.position.z), 1.5f));
            }
            else
            {
                StartCoroutine(spike.changePosition(new Vector3(spike.gameObject.transform.position.x, 2.5f, spike.gameObject.transform.position.z), 3f));
            }

        }
    }

    private void restOfDrop()
    {
        //Check for pops
        Debug.Log("Yo, we checking for pops");
    }

   
}

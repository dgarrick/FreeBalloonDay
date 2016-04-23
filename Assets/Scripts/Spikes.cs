using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

        //Turn off guide lights
        Block[] blocks = GameObject.FindObjectsOfType<Block>();
        foreach (Block block in blocks)
        {
            Destroy(block.gameObject.transform.GetChild(0).gameObject);
            Destroy(block.gameObject.transform.GetChild(1).gameObject);
        }

        //Drop the spikes!
        Spike[] spikes = GameObject.FindObjectsOfType<Spike>();
        foreach (Spike spike in spikes)
        {
            // Figure out if they collide or not
            bool matchingBlock = false;
            foreach (Block block in blocks)
            {
                if (block.transform.position.x == spike.transform.position.x && block.transform.position.z == spike.transform.position.z)
                {
                    matchingBlock = true;
                }
            }
            if (matchingBlock)
            {
                float time = Random.Range(4, 10) / 10f;
                StartCoroutine(spike.changePosition(new Vector3(spike.gameObject.transform.position.x, 35, spike.gameObject.transform.position.z), time));
            }   
            else
            {
                float time = Random.Range(8, 18) / 10f;
                StartCoroutine(spike.changePosition(new Vector3(spike.gameObject.transform.position.x, 2.5f, spike.gameObject.transform.position.z), time));
            }

        }
    }
   
}

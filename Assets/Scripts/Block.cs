﻿using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	Block[] blocks;
	BoxCollider[] colliders;
	Vector3 myPosition;
	GameObject middle;
	public bool isIce;
	public bool isImmovable;
	// Use this for initialization
	void Start () {
		blocks = GameObject.FindObjectsOfType<Block> ();
		middle = GameObject.Find ("Middle");
		myPosition = gameObject.transform.position;
		colliders = GetComponents<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Player" && !isImmovable)
        {
            Vector3 myPos = gameObject.transform.position;
            Vector3 playerPos = other.gameObject.transform.position;
            Vector3 diff = playerPos - myPos;
            bool xLarger = Mathf.Abs(diff.x) > Mathf.Abs(diff.z);
			if (xLarger && diff.x > 0)
				tryMove (new Vector3(-4, 0, 0));
			else if (xLarger && diff.x < 0)
				tryMove(new Vector3 (4, 0, 0));
			else if (!xLarger && diff.z > 0)
				tryMove(new Vector3(0, 0, -4));
            else if (!xLarger && diff.z < 0)
				tryMove(new Vector3(0, 0, 4));
            else
                Debug.Log("Something very bad happened");
        }
    }

	private void tryMove(Vector3 direction) {
		// Normal blocks. Just move one unit in direction
		if (!isIce) {
			Vector3 newPosition = direction + myPosition;
			if (!hasBlock (newPosition) && isInBounds (newPosition))
				StartCoroutine (changePosition (newPosition));
		}
		// Ice blocks. Move until they can no longer move.
		else if (isIce) {
			Vector3 newPosition = myPosition;
			while (!hasBlock (newPosition + direction) && isInBounds(newPosition + direction)) {
				newPosition += direction;
			}
			StartCoroutine (changePosition(newPosition));
		}
	}

	public bool isInBounds(Vector3 position) {
		Vector3 middleScale = middle.transform.localScale;
		if (Mathf.Abs (middleScale.x / 2) < Mathf.Abs (position.x) || Mathf.Abs (middleScale.z / 2) < Mathf.Abs (position.z))
			return false;
		return true;
	}

	public bool hasBlock(Vector3 position) {
		bool ret = false;
		foreach (Block block in blocks) {
			Vector3 blockPos = block.gameObject.transform.position;
			if (position.x == blockPos.x && position.y == blockPos.y && position.z == blockPos.z)
				ret = true;
		}
		return ret;
	}

	private IEnumerator changePosition(Vector3 newPos) {
        foreach (BoxCollider coll in colliders)
        {
            coll.enabled = false;
        }
		// Blocks are 4 units. 4 units = 0.5 seconds.
		float time = Vector3.Distance (newPos, myPosition) / 8.0f; 
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
		myPosition = gameObject.transform.position;
    }
}

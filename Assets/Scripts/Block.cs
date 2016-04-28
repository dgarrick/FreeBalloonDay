using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	Block[] blocks;
	BoxCollider[] colliders;
	Vector3 myPosition;
	GameObject middle;
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
        if (other.gameObject.tag == "Player")
        {
            Vector3 myPos = gameObject.transform.position;
            Vector3 playerPos = other.gameObject.transform.position;
            Vector3 diff = playerPos - myPos;
            bool xLarger = Mathf.Abs(diff.x) > Mathf.Abs(diff.z);
			if (xLarger && diff.x > 0)
				tryMove (new Vector3(-4, 0, 0) + myPosition);
			else if (xLarger && diff.x < 0)
				tryMove(new Vector3 (4, 0, 0) + myPosition);
			else if (!xLarger && diff.z > 0)
				tryMove(new Vector3(0, 0, -4) + myPosition);
            else if (!xLarger && diff.z < 0)
				tryMove(new Vector3(0, 0, 4) + myPosition);
            else
                Debug.Log("Something very bad happened");
        }
    }

	private void tryMove(Vector3 position) {
		if (!hasBlock (position) && isInBounds(position))
			StartCoroutine (changePosition (position));
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
		myPosition = gameObject.transform.position;
    }
}

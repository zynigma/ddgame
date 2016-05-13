using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour {

	int numBGPanels = 6;
	int numWaves = 7;
	int numWater = 7;

	float pipeMax = 5f;
	float pipeMin = -3f;

	public GameObject[] dabbableBricks;

	void Start() {
		GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");

		foreach(GameObject pipe in pipes) {
			Vector3 pos = pipe.transform.position;
			pos.y = Random.Range(pipeMin, pipeMax);
			pipe.transform.position = pos;

		}

		// Make a variable to hold all of the dabbable bricks in the scene
		dabbableBricks = GameObject.FindGameObjectsWithTag ("dabbable");


	}

	void OnTriggerEnter2D(Collider2D collider) {
		//Debug.Log ("Triggered: " + collider.name);

		//float widthOfBGObject = ((BoxCollider2D)collider).size.x;

		if (collider.tag == "waves") {
			Debug.Log ("Move Waves 2.346f per clone here. Right now there are four clones, so we will move them 9.384f so that they loop correctly.");
			Vector3 pos = collider.transform.position;
			pos.x += 9.384f;
			collider.transform.position = pos;


		}else if (collider.tag == "water"){
			Debug.Log ("Move Water 2.346f per clone here. Right now there are four clones, so we will move them 9.384f so that they loop correctly.");
			Vector3 pos = collider.transform.position;
			pos.x += 9.384f;
			collider.transform.position = pos;

		}else {
			//the looper has hit the ground or background, so move it according to is width. Intro to this project.
			Debug.Log ("Triggered: " + collider.name);

			float widthOfBGObject = ((BoxCollider2D)collider).size.x;

			Vector3 pos = collider.transform.position;

			pos.x += widthOfBGObject * numBGPanels;

			if (collider.tag == "Pipe") {
				pos.y = Random.Range (pipeMin, pipeMax);

				foreach (GameObject dabbable in dabbableBricks){
					dabbable.SetActive(true);
					dabbable.transform.GetComponent<SpriteRenderer>().enabled = true;
				}

			}

			collider.transform.position = pos;
		}

	}
}

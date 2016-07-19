using UnityEngine;
using System.Collections;

public class BGLooperWaveCatch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		//Debug.Log ("Triggered: " + collider.name);
		
		//float widthOfBGObject = ((BoxCollider2D)collider).size.x;
		
		if (collider.tag == "waves") {
			//Debug.Log ("Move Waves 2.346f per clone here. Right now there are four clones, so we will move them 9.384f so that they loop correctly.");
			Vector3 pos = collider.transform.position;
			pos.x -= 9.384f;
			collider.transform.position = pos;
		}
	}
}

using UnityEngine;
using System.Collections;

public class MiniBrick : MonoBehaviour {

	public Transform waterTrail;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (waterTrail == null)
			waterTrail = transform.FindChild ("GameObject");
		//scale the rock depending on it's depth. The game is orthographic, so when we move the rocks along the z axis they don't scale up.
		//To mimic depth, we will scale the rock based on it's z position
		transform.localScale = new Vector3 (transform.position.z + 0.0089f, transform.position.z + 0.0089f, transform.position.z + 0.0089f);

		//If the rock is aobut to leave the screen, detach the waterTrail particle system
		if (transform.position.y < -0.05f) {
			waterTrail.GetComponent<ParticleSystem>().Stop();
			waterTrail.transform.parent = null;
		}

		//if the rock fragment is close to the screen so it is too big, make it sink to the bottom of water
		if (transform.position.z < -0.025f) {
			GetComponent<Rigidbody>().AddForce(new Vector3(0,-0.75f,0));
		}

		//if a rock wants to leave the top of the water, apply more gravity to it so it sinks
		if (transform.position.y > 1.5f) {
			GetComponent<Rigidbody>().AddForce(new Vector3(0,-1.3f,0));
		}
	}

	void ScaleBrick(){
		//iTween.ScaleTo(gameObject, iTween.Hash("scale", 
	}

	void DisappearBrick(){
		Color tempcolor = gameObject.GetComponent<MeshRenderer> ().material.color;

		tempcolor.a = Mathf.MoveTowards(0, 1, Time.deltaTime);
		gameObject.GetComponent<MeshRenderer> ().material.color = tempcolor;
	}
}

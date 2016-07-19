using UnityEngine;
using System.Collections;

public class ScorePoint : MonoBehaviour {

	bool isColliding;
	public AudioClip a_dip;
	public AudioSource audioSource;


	void Start (){

	}
	/*
	void OnTriggerEnter2D(Collider2D collider) {

		if(isColliding) return;

		isColliding = true;
		//Rest of code

		if(collider.tag == "Player") {
			Score.AddPoint(2);

		}

		if(collider.tag == "PlayerDip") {
			Score.AddPoint(1);


		}

		if(collider.tag == "PlayerDab") {




		}


		if(collider.tag == "PlayerDive") {
			Score.AddPoint(3);


		}
		/*
		if (collider.name == "ScoreHit") {
			print ("hit");
				audioSource.PlayOneShot (a_dip);
		}

	}
*/
	void Update(){
		isColliding = false;
	}
}

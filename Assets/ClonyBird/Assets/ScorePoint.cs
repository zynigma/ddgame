using UnityEngine;
using System.Collections;

public class ScorePoint : MonoBehaviour {




	void Start (){

	}

	void OnTriggerEnter2D(Collider2D collider) {
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
	}
}

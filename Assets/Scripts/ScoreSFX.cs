using UnityEngine;
using System.Collections;
using TextFx;

public class ScoreSFX : MonoBehaviour {

	public AudioClip a_dip;
	public AudioSource audioSource;
	public GameObject player;
	public GameObject scorePopUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.transform.tag == "scoreBox" && player.tag != "PlayerDab") {
			//audioSource.PlayOneShot (a_dip);

			//Instantiate the score popup, and change the text of it depending on the type of score, but only if we are NOT dabbing. The dab text will be done when the brick is hit
		
			GameObject newScorePopUp = Lean.LeanPool.Spawn(scorePopUp, player.transform.position, Quaternion.identity);
			newScorePopUp.transform.parent = Camera.main.transform;

			if(transform.position.y < 0.3f){
				iTween.MoveBy(newScorePopUp, iTween.Hash("x", 1.4f, "y", Random.Range(-0.2f,0.9f), "time", 5));
			}
			else if(transform.position.y > 1.2f){
				iTween.MoveBy(newScorePopUp, iTween.Hash("x", 1.4f, "y", Random.Range(-0.9f,0.2f), "time", 5));
			}
			else{
				iTween.MoveBy(newScorePopUp, iTween.Hash("x", 1.4f, "y", Random.Range(-0.6f,0.6f), "time", 5));
			}


			if(player.tag == "Player") {
				Score.AddPoint(2);
				newScorePopUp.GetComponent<TextFxNative>().Text = "what?";
			}
			
			if(player.tag == "PlayerDip") {
				Score.AddPoint(2);
				newScorePopUp.GetComponent<TextFxNative>().Text = "2";

				
			}
			/*
			if(player.tag == "PlayerDab") {
				
				
				
				
			}
			*/
			
			if(player.tag == "PlayerDive") {
				Score.AddPoint(4);
				newScorePopUp.GetComponent<TextFxNative>().Text = "4";

				
			}
		}
		
	}
}

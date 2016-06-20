using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	static int score = 0;
	static int highScore = 0;

	static Score instance;

	static public void AddPoint(int points) {
		if(instance.bird.dead)
			return;

		score += points;

		if(score > highScore) {
			highScore = score;
		}
	}

	BirdMovement bird;
	bool android;

	void Start() {
		if (Application.platform == RuntimePlatform.Android){
			android = true;
			GetComponent<GUIText>().fontSize = Screen.width/30;
		}


		instance = this;
		GameObject player_go = GameObject.FindGameObjectWithTag("Player");
		if(player_go == null) {
			Debug.LogError("Could not find an object with tag 'Player'.");
		}

		bird = player_go.GetComponent<BirdMovement>();
		score = 0;
		highScore = PlayerPrefs.GetInt("highScore", 0);
	}

	void OnDestroy() {
		instance = null;
		PlayerPrefs.SetInt("highScore", highScore);
	}

	void Update () {
		GetComponent<GUIText>().text = "Score: " + score + "\nHigh Score: " + highScore;
	}
}

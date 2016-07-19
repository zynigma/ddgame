using UnityEngine;
using System.Collections;
using TextFx;

public class Score : MonoBehaviour {

	public AudioClip a_dip;
	public AudioSource audioSource;

	public GUIText scoreText;

	static int score = 0;
	static int highScore = 0;

	static Score instance;

	static public void AddPoint(int points) {
		if(instance.bird.dead)
			return;

		score += points;

		//instance.audioSource.PlayOneShot (instance.a_dip);

		if(score > highScore) {
			highScore = score;
		}

		/*
		iTween.ValueTo( gameObject, iTween.Hash(
			"from", exampleInt,
			"to", 100,
			"time", 1f,
			"onupdatetarget", gameObject,
			"onupdate", "tweenOnUpdateCallBack",
			"easetype", iTween.EaseType.easeOutQuad
			)
		               )
		 */
	}

	BirdMovement bird;
	bool android;

	public TextFxNative scoreTextFx;

	void Start() {
		GetComponent<GUIText>().fontSize = Screen.width/30;

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
		GetComponent<GUIText>().text = "High Score: " + highScore + "\nScore: " + score ;
		//scoreTextFx.Text = "High Score: " + highScore + "\nScore: " + score ;
	}
}

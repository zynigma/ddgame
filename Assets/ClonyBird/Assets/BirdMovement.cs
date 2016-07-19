using UnityEngine;
using System.Collections;
using TextFx;

public class BirdMovement : MonoBehaviour {

	public Camera mainCamera;
	Vector3 velocity = Vector3.zero;
	public float flapSpeed    = 100f;
	public float forwardSpeed = 1f;
	public Rigidbody2D ddRigidbody;

	bool didDip = false;
	bool didDab = false;
	bool didDive = false;
	bool didDart = false;
	bool isDabbing = false;
	bool didBreakBrick = false;
	public bool scoreDip;
	public bool scoreDive;
	public bool sccoreDab;
	bool isZoomedOut = false;

	Animator animator;

	public bool dead = false;
	float deathCooldown;

	public bool godMode = false;

	public GameObject ddgo;
	public GameObject cameraGO;
	public GameObject[] waves;
	public float waveSpeed;


	public Sprite neutralSprite;
	public Sprite dabSprite;
	bool android;
	public int dabCount;
	public int dabCountRow;
	public int dabCountRowMultiplier;
	public int dipCount;
	public int diveCount;
	public int dartCount;

	public GameObject miniBrick;
	public GameObject scorePopUp;
	public GameObject[] dabMultiplierIcons;


	// Use this for initialization
	void Start () {
		if (Application.platform == RuntimePlatform.Android){
			android = true;
		
		}
		animator = transform.GetComponentInChildren<Animator>();

		if(animator == null) {
			Debug.LogError("Didn't find animator!");
		}

		ddRigidbody = GetComponent<Rigidbody2D> ();

		//Set a random wave speed for all the waves to use
		waveSpeed = Random.Range (-0.015f, 0.015f);
	}

	// Do Graphic & Input updates here
	void Update() {
		if(dead) {
			deathCooldown -= Time.deltaTime;

			if(deathCooldown <= 0) {
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
					Application.LoadLevel( Application.loadedLevel );
				}
			}
		}
		else {

			if (android){
				for (var i = 0; i < Input.touchCount; ++i) {
					Touch touch = Input.GetTouch(i);
					if (touch.phase == TouchPhase.Began) {
						if (touch.position.x > (Screen.width/2)) {
							//If we have tapped the right side of the screen on bottom 1/3 of screen.
							if(touch.position.y <= Screen.height/3){
								didDive = true;
							}

							//If we have tapped the right side of the screen on the top 2/3 of screen.
							if(touch.position.y > Screen.height/3){
								didDip = true;
							}
						}
						
						if (touch.position.x <= (Screen.width/2)) {
							//If we have tapped the left side of the screen
							didDab = true;
						}
					}
				}
			}

			else{
				//if we are not using Android (so we are on the computer)
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
					didDip = true;
				}

				if(Input.GetMouseButtonDown(2) ) {
					didDab = true;
				}

				if(Input.GetMouseButtonDown(1) ) {
					didDive = true;
				}
			}
		}



		
		//Debug.Log (GetComponent<Rigidbody2D> ().velocity.y);
		//Debug.Log (ddRigidbody.velocity.x);

		//Move the camera out when dd is above the water line (1.85f);
		//Zoom In
		/******************************************************************************************************
		if (ddgo.transform.position.y < 1.9f && isZoomedOut) {
			Vector3 newCameraPos = mainCamera.transform.position;
			newCameraPos.y = 0.82f;
			//mainCamera.transform.position = newCameraPos;
			isZoomedOut = false;
			ddRigidbody.gravityScale = -0.3f;
			print (ddRigidbody.gravityScale);
			iTween.MoveTo (cameraGO, iTween.Hash ("position", newCameraPos, "time", 0.15f, "onCompleteTarget", gameObject, "onComplete", "SetZoomedOutFalse"));
		}

		//Zoom Out
		else if (ddgo.transform.position.y > 1.6f && !isZoomedOut) {
			Vector3 newCameraPos = mainCamera.transform.position;
			newCameraPos.y = 2.4f;
			//mainCamera.transform.position = newCameraPos;
			//isZoomedOut = true;
			ddRigidbody.gravityScale = 0.15f;
			print (ddRigidbody.gravityScale);
			
			iTween.MoveTo (cameraGO, iTween.Hash ("position", newCameraPos, "time", 0.15f, "onCompleteTarget", gameObject, "onComplete", "SetZoomedOutTrue"));
		} else {
		}

		*************************************************************************************************/
			//print ("dummy area");
	}

	void SetZoomedOutTrue(){
		isZoomedOut = true;
	}

	void SetZoomedOutFalse(){
		isZoomedOut = false;
	}

	
	// Do physics engine updates here
	void FixedUpdate () {

		if (dead)
			return;


		ddRigidbody.AddForce (Vector2.right * forwardSpeed);

		if (didDip) {

			if(dipCount == 0){

				//Made rigidbody react to gravity by making kinesmatic false
				ddRigidbody.isKinematic = false;

				//if this is the first dip, move the waves

					foreach(GameObject wave in waves)
						wave.GetComponent<MoveWave>().moveSpeed = waveSpeed;
					//print (wave.name);


			}
			else{

				TurnOffDab ();

				ddRigidbody.angularVelocity = 0;

				ddRigidbody.AddForce (-Vector2.up * flapSpeed);

				animator.SetTrigger ("DoFlap");

				ddgo.transform.rotation = Quaternion.Euler (0, 0, 90);

				didDip = false;

				//change the tag of this object to reflect that we are in dip mode
				gameObject.tag = "PlayerDip";

				//Reset dabs in a row to 1
				dabCountRow = 1;
				dabCountRowMultiplier = 1;

				//turn off all of the dab multiplier icons because the streak has been broken
				foreach(GameObject icon in dabMultiplierIcons){
					icon.SetActive(false);
				}


			}

			dipCount += 1;

		}


		//If the dive button has been pressed and we have dipped at least once)
		if (didDive && dipCount > 0) {
			TurnOffDab ();

			ddRigidbody.angularVelocity = 0;

			ddRigidbody.AddForce (-Vector2.up * (flapSpeed * 1.6f));
			
			animator.SetTrigger ("DoFlap");

			ddgo.transform.rotation = Quaternion.Euler (0, 0, 90);

			
			didDive = false;
			//transform.rotation = Quaternion.Euler(0, 0, 90);

			//change the tag of this object to reflect that we are in dive mode
			gameObject.tag = "PlayerDive";

			//Reset dabs in a row to 1
			dabCountRow = 1;
			dabCountRowMultiplier = 1;

			//turn off all of the dab multiplier icons because the streak has been broken
			foreach(GameObject icon in dabMultiplierIcons){
				icon.SetActive(false);
			}

		}


		//If the dab button has been pressed and the player has dipped at least one time
		if (didDab && dipCount > 0) {

			//if we've started dabbing, do the animation and then stop. Without this trigger, it will play the animation every frame for the Invoke function's time.
			if (!isDabbing) {
				animator.SetTrigger ("DoDab");
				isDabbing = true;
			}

			//Stop dd from turning
			ddRigidbody.angularVelocity = 0;

			//Neutralize current velocity
			ddRigidbody.velocity = Vector3.zero;

			// set dd rotation for dab
			ddgo.transform.rotation = Quaternion.Euler (0, 0, 6.1f);

			// make dd charge forward
			ddRigidbody.AddForce (Vector2.right * forwardSpeed * 110);

			//timer that will turn the tab mode off in the game. Does not affect scoring
			Invoke ("TurnOffDab", 0.4f);

			//change the tag of this object to reflect that we are in dab mode
			gameObject.tag = "PlayerDab";


		}

		//THIS IS MY LATEST TRY AT CUSTOMIZING THIS

		//If we are NOT dabbing, rotate dd to match his velocity
		if (!didDab && dipCount > 0) {
			if (ddRigidbody.velocity.y > 0) {
				float angle = Mathf.LerpAngle (0, 120, (ddRigidbody.velocity.y / 3f));
				transform.rotation = Quaternion.Euler (0, 0, angle);

			}

			if (ddRigidbody.velocity.y < 0) {
				float angle = Mathf.LerpAngle (360, 280, (-ddRigidbody.velocity.y / 3f));
				transform.rotation = Quaternion.Euler (0, 0, angle);
			
			}

		}
	}

	void TurnOffDab() {
		//turn dab off
		isDabbing = false;
		didDab = false;

		//Return velocity of dd to max movement speed
		//ddRigidbody.velocity = new Vector2(1.2f , 0);
		Vector3 temp = ddRigidbody.velocity;
		temp.x = 1.2f;
		ddRigidbody.velocity = temp;
	}

	void OnCollisionEnter2D(Collision2D collision) {


		if (collision.transform.tag == "dabbable" && isDabbing) {


			int randomMiniBrickCount = Random.Range (1,4);


			for (int x = 0; x < randomMiniBrickCount; x++) {
				var newMiniBrick = Lean.LeanPool.Spawn(miniBrick, collision.transform.position, Quaternion.identity);
				//iTween.MoveTo(newMiniBrick, iTween.Hash("position", new Vector3(transform.position.x + 50, Random.Range(-100,100), Random.Range(-100,100))));
				newMiniBrick.GetComponent<Rigidbody>().AddForce(Random.Range(15,130), Random.Range(-30,45), Random.Range(-1.5f,0));
				newMiniBrick.GetComponent<Rigidbody>().AddTorque(Random.Range(-350,350), Random.Range(-350,350), Random.Range(-350,350));
				Lean.LeanPool.Despawn(newMiniBrick, 15);
			}


			//collision.transform.GetComponent<SpriteRenderer>().enabled = false;
			collision.gameObject.SetActive(false);

			dabCount += 1;

			//Turn on break brick boolean so that we increase the number of dabs in a row
			didBreakBrick = true;

			Score.AddPoint(dabCountRowMultiplier);

			GameObject newScorePopUp = Lean.LeanPool.Spawn(scorePopUp, transform.position, Quaternion.identity);
			newScorePopUp.transform.parent = Camera.main.transform;
			if(transform.position.y < 0.3f){
				iTween.MoveBy(newScorePopUp, iTween.Hash("x", Random.Range(1.4f, 2.4f), "y", Random.Range(-0.2f,1), "time", 5));
			}
			else if(transform.position.y > 1.2f){
				iTween.MoveBy(newScorePopUp, iTween.Hash("x", Random.Range(1.4f, 2.4f), "y", Random.Range(-1,0.2f), "time", 5));
			}
			else{
				iTween.MoveBy(newScorePopUp, iTween.Hash("x", Random.Range(1.4f, 2.4f), "y", Random.Range(-0.7f,0.7f), "time", 5));
			}
			newScorePopUp.GetComponent<TextFxNative>().Text = dabCountRowMultiplier.ToString();
		}



		if(godMode || isDabbing)
			return;

		animator.SetTrigger("Death");
		dead = true;
		deathCooldown = 0.5f;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		//If we go through the score box and we have broken a brick, add 1 to the number of dabs we have in a row
		if (collider.transform.tag == "scoreBox" && didBreakBrick) {

			/*
			dabCountRow += 1;
			dabCountRowMultiplier += 2;
			//Turn off the break brick boolean so that it can be triggered again.
			didBreakBrick = false;
			*/

			//Turn on the dab multiplier icons to match the number of dabs in a row
			if(dabCountRow == 1) dabMultiplierIcons[0].SetActive(true);
			if(dabCountRow == 2) dabMultiplierIcons[1].SetActive(true);
			if(dabCountRow == 3) dabMultiplierIcons[2].SetActive(true);
			if(dabCountRow == 4) dabMultiplierIcons[3].SetActive(true);
			if(dabCountRow == 5) dabMultiplierIcons[4].SetActive(true);
			if(dabCountRow == 6) dabMultiplierIcons[5].SetActive(true);
			if(dabCountRow == 7) dabMultiplierIcons[6].SetActive(true);

		}

	}

	void OnTriggerExit2D(Collider2D collider){
		if (collider.transform.tag == "scoreBox" && didBreakBrick) {
			
			dabCountRow += 1;
			dabCountRowMultiplier += 2;
			//Turn off the break brick boolean so that it can be triggered again.
			didBreakBrick = false;
			//Turn on the dab multiplier icons to match the number of dabs in a row
			/*
			if(dabCountRow == 2) dabMultiplierIcons[0].SetActive(true);
			if(dabCountRow == 3) dabMultiplierIcons[1].SetActive(true);
			if(dabCountRow == 4) dabMultiplierIcons[2].SetActive(true);
			if(dabCountRow == 5) dabMultiplierIcons[3].SetActive(true);
			if(dabCountRow == 6) dabMultiplierIcons[4].SetActive(true);
			if(dabCountRow == 7) dabMultiplierIcons[5].SetActive(true);
			if(dabCountRow == 8) dabMultiplierIcons[6].SetActive(true);
			*/
			
		}
	}
}

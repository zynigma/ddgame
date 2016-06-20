using UnityEngine;
using System.Collections;

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
	public int dipCount;
	public int diveCount;
	public int dartCount;



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

				//Reset dabs in a row to 0
				dabCountRow = 0;

				//if this is the first dip, move the waves
				if(dipCount < 1) {
					foreach(GameObject wave in waves)
						wave.GetComponent<MoveWave>().moveSpeed = waveSpeed;
						//print (wave.name);
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

			//Reset dabs in a row to 0
			dabCountRow = 0;
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
			ddgo.transform.rotation = Quaternion.Euler (0, 0, 0);

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

			//collision.transform.GetComponent<SpriteRenderer>().enabled = false;
			collision.gameObject.SetActive(false);

			dabCountRow += 1;

			Score.AddPoint(1);
		
		}

		if(godMode || isDabbing)
			return;

		animator.SetTrigger("Death");
		dead = true;
		deathCooldown = 0.5f;
	}


}

using UnityEngine;
using System.Collections;

public class MoveWave : MonoBehaviour {

	public float moveSpeed;
	// Use this for initialization
	void Start () {
		//moveSpeed = Random.Range (-0.015f, 0.015f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(moveSpeed,0,0);
	}
}

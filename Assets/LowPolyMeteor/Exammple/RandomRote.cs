using UnityEngine;
using System.Collections;

public class RandomRote : MonoBehaviour {

	private Vector3 rotationSpeed;
	// Use this for initialization
	void Start () {
	
		rotationSpeed = new Vector3(Random.Range(5,15),Random.Range(5,15),Random.Range(5,15));
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate( rotationSpeed * Time.deltaTime);
	}
}

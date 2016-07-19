using UnityEngine;
using System.Collections;

public class RoateCamera : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		transform.RotateAround( Vector3.zero,Vector3.up, 15 * Time.deltaTime);	
		transform.RotateAround( Vector3.zero,Vector3.left, 5 * Time.deltaTime);	

		transform.LookAt( new Vector3(0,5,0));
	}
}

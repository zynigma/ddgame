using UnityEngine;
using System.Collections;

public class BGChooser : MonoBehaviour {


	public Sprite[] backgroundSprites;
	public SpriteRenderer[] backgroundObjects;

	// Use this for initialization
	void Start () {
		int newBackgroundColor = Random.Range (0, backgroundSprites.Length);

		foreach (SpriteRenderer backgroundObject in backgroundObjects) {
			backgroundObject.sprite = backgroundSprites[newBackgroundColor];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

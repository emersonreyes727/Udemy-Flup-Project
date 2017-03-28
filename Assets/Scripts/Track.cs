using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

	[SerializeField] private float resetPosition; 
	[SerializeField] private float startPosition; 
	[SerializeField] private float speed = 5.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	public virtual void Update () {
		if (GameManager.instance.EnterGame && !GameManager.instance.GameOver) {
			// moves the track 
			transform.Translate (Vector3.left * (speed * Time.deltaTime));

			// resets the track
			if (transform.localPosition.x < resetPosition) {
				Vector3 newPosition = new Vector3 (startPosition, transform.position.y, transform.position.z);
				transform.position = newPosition;
			}
		} 	
	}
}

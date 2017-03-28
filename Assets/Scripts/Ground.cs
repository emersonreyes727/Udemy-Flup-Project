using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : Track {

	[SerializeField] private Vector3 topPosition;
	[SerializeField] private Vector3 bottomPosition; 
	[SerializeField] private float rockSpeed; 
	
	// Use this for initialization
	void Start () {
		StartCoroutine (Move (bottomPosition));		
	}

	// Update is called once per frame
	public override void Update () {
			// ground inherits from track update()
			base.Update ();
	}

	IEnumerator Move(Vector3 target) {
		// moves the ground up and down
		while (Mathf.Abs((target - transform.localPosition).y) > 0.20f) {
			Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;
			transform.localPosition += direction * (rockSpeed * Time.deltaTime);
			yield return null;
		}

		yield return new WaitForSeconds (0.50f);

		Vector3 newDirection = target.y == topPosition.y? bottomPosition : topPosition;

		StartCoroutine (Move (newDirection));
	}
}

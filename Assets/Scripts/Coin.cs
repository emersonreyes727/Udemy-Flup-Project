using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Track {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
		// coin inherits from track so that it'll move
		base.Update ();
	}
}

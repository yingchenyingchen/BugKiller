using UnityEngine;
using System.Collections;

public class Flashlight : Equiptment {

	public Light lightBulb;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Activate()
	{
		lightBulb.enabled = !lightBulb.enabled;
	}

}

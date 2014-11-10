using UnityEngine;
using System.Collections;

public class Flashlight : Equipment {

	public Light lightBulb;

	// Use this for initialization
	void Start () {
		Equipped = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//toggle lightbulb
	public override void Activate()
	{
		lightBulb.enabled = !lightBulb.enabled;
	}

	public override void OnEquip()
	{
		Equipped = true;
	}

	public override void OnUnequip()
	{

	}
}

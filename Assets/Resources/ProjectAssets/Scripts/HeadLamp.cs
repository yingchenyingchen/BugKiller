using UnityEngine;
using System.Collections;

public class HeadLamp : Equipment {

	public Light lamp;

	void Start ()
	{

	}
	
	
	void Update () {
		

	}
		
	public override void Activate ()
	{
		lamp.enabled = !lamp.enabled;
	}


	public override void OnEquip ()
	{


	}


	public override void OnUnequip ()
	{

	}
}

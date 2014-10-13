﻿using UnityEngine;
using System.Collections;

public class EquiptmentHandler : MonoBehaviour {

	public Equiptment EquiptmentHeld;
	public GameObject RightHand;

	// Use this for initialization
	void Start () {
		//Equip (EquiptmentHeld);
	}
	
	// Update is called once per frame
	void Update () {
		if (EquiptmentHeld && Input.GetMouseButtonDown(0))
						ActivateEquiptmentHeld ();

	}

	public void Equip(Equiptment equipment)
	{
		Unequip ();
		EquiptmentHeld = (Equiptment)Instantiate(equipment);
		EquiptmentHeld.transform.position = RightHand.transform.position;
		EquiptmentHeld.transform.rotation = RightHand.transform.rotation;
		EquiptmentHeld.transform.parent = RightHand.transform;
	}

	public void Unequip()
	{
		if(EquiptmentHeld)
			DestroyImmediate(EquiptmentHeld.gameObject);
		EquiptmentHeld = null;
	}


	void ActivateEquiptmentHeld()
	{
		EquiptmentHeld.Activate ();
	}
}

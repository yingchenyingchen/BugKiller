using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	public List<Equiptment> EquipmentList { get; private set; }

	// Use this for initialization
	void Start () {
		EquipmentList = new List<Equiptment> ();
	}

	public void AddItem(Equiptment equiptment)
	{
		EquipmentList.Add (equiptment);
	}


	// Update is called once per frame
	void Update () {
	
	}
}

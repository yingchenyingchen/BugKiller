using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	public List<Equipment> EquipmentList { get; private set; }

	// Use this for initialization
	void Start () {
		EquipmentList = new List<Equipment> ();
	}

	public void AddItem(Equipment equipment)
	{
		EquipmentList.Add (equipment);
	}


	// Update is called once per frame
	void Update () {
	
	}
}

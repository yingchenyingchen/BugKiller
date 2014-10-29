using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	public QuickInventory EquipmentList { get; private set; }
	public Equipment defaultItem;

	// Use this for initialization
	void Start () {
		EquipmentList = new QuickInventory (10);
	}

	/*
	public void AddItem(Equipment equipment)
	{
		EquipmentList.Add (equipment);
	}*/


	// Update is called once per frame
	void Update () {
	
	}
}

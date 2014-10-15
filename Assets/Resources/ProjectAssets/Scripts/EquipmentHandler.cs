using UnityEngine;
using System.Collections;

public class EquipmentHandler : MonoBehaviour {

	public Equipment EquipmentHeld;
	public GameObject RightHand;

	// Use this for initialization
	void Start () {
		//Equip (EquiptmentHeld);
	}
	
	// Update is called once per frame
	void Update () {
		if (EquipmentHeld && Input.GetMouseButtonDown(0))
						ActivateEquipmentHeld ();

	}

	public void Equip(Equipment equipment)
	{
		Unequip ();
		EquipmentHeld = (Equipment)Instantiate(equipment);
		EquipmentHeld.transform.position = RightHand.transform.position;
		EquipmentHeld.transform.rotation = RightHand.transform.rotation;
		EquipmentHeld.transform.parent = RightHand.transform;
	}

	public void Unequip()
	{
		if(EquipmentHeld)
			DestroyImmediate(EquipmentHeld.gameObject);
		EquipmentHeld = null;
	}


	void ActivateEquipmentHeld()
	{
		EquipmentHeld.Activate ();
	}
}

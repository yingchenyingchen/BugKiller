using UnityEngine;
using System.Collections;

public class EquipmentHandler : MonoBehaviour {

	public Equipment EquipmentHeldRight;
	public Equipment EquipmentHeldLeft;
	public GameObject RightHand;
	public GameObject LeftHand;
	public Equipment HandModelLeft;
	public Equipment HandModelRight;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (EquipmentHeldLeft && Input.GetMouseButtonDown(0))
			EquipmentHeldLeft.Activate (KeyCode.Mouse0);
		if (EquipmentHeldRight && Input.GetMouseButtonDown(1))
			EquipmentHeldRight.Activate (KeyCode.Mouse1);

	}

	public void EquipRight(Equipment equipment)
	{
		if(checkEquipped(equipment, "right"))
			return;
		if (equipment.gameObject.name == "LHand")
			return;

		UnequipRight ();
		EquipmentHeldRight = (Equipment)Instantiate(equipment);
		EquipmentHeldRight.transform.position = RightHand.transform.position;
		EquipmentHeldRight.transform.rotation = RightHand.transform.rotation;
		EquipmentHeldRight.transform.parent = RightHand.transform;
	}

	public void EquipLeft(Equipment equipment)
	{
		if(checkEquipped(equipment, "left"))
			   return;
		if (equipment.gameObject.name == "RHand")
			return;
		UnequipLeft ();
		EquipmentHeldLeft = (Equipment)Instantiate(equipment);
		EquipmentHeldLeft.transform.position = LeftHand.transform.position;
		EquipmentHeldLeft.transform.rotation = LeftHand.transform.rotation;
		EquipmentHeldLeft.transform.parent = LeftHand.transform;
	}

	//Deals with any equipped items that are a clone of param equipment
	//if such equipment is equipped to the hand that we are trying to equip to, return true
	//otherwise return false and if such equipment is equipped to the other hand, unequip it.
	private bool checkEquipped(Equipment equipment, string hand)
	{
		Equipment thisHand, otherHand;
		string name = equipment.name + "(Clone)";
		System.Action unequip;
		if (hand == "right")
		{
			otherHand = EquipmentHeldLeft;
			thisHand = EquipmentHeldRight;
			unequip = () => UnequipLeft();
		} 
		else
		{
			otherHand = EquipmentHeldRight;
			thisHand = EquipmentHeldLeft;
			unequip = () =>  UnequipRight();
		}
		if (thisHand && thisHand.name == name) //if there is something equipped in hand and its name is the same 
		{
			return true;
		} 
		else if (otherHand && otherHand.name == name)
			unequip ();
		return false;
	}

	public void UnequipRight()
	{
		if(EquipmentHeldRight)
			DestroyImmediate(EquipmentHeldRight.gameObject);
		EquipmentHeldRight = null;
	}

	public void UnequipLeft()
	{
		if(EquipmentHeldLeft)
			DestroyImmediate(EquipmentHeldLeft.gameObject);
		EquipmentHeldLeft = null;
	}
}

using UnityEngine;
using System.Collections;

public class EquipmentHandler : MonoBehaviour {

	public Equipment EquipmentHeldRight;
	public Equipment EquipmentHeldLeft;
	public GameObject RightHand;
	public GameObject LeftHand;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (EquipmentHeldLeft && Input.GetMouseButtonDown(0))
			ActivateEquipmentHeldLeft ();
		if (EquipmentHeldRight && Input.GetMouseButtonDown(1))
			ActivateEquipmentHeldRight ();

	}

	public void EquipRight(Equipment equipment)
	{
		UnequipRight ();
		EquipmentHeldRight = (Equipment)Instantiate(equipment);
		EquipmentHeldRight.transform.position = RightHand.transform.position;
		EquipmentHeldRight.transform.rotation = RightHand.transform.rotation;
		EquipmentHeldRight.transform.parent = RightHand.transform;
	}

	public void EquipLeft(Equipment equipment)
	{
		UnequipLeft ();
		EquipmentHeldLeft = (Equipment)Instantiate(equipment);
		EquipmentHeldLeft.transform.position = LeftHand.transform.position;
		EquipmentHeldLeft.transform.rotation = LeftHand.transform.rotation;
		EquipmentHeldLeft.transform.parent = LeftHand.transform;
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


	void ActivateEquipmentHeldRight()
	{
		EquipmentHeldRight.Activate ();
	}

	void ActivateEquipmentHeldLeft()
	{
		EquipmentHeldLeft.Activate ();
	}
}

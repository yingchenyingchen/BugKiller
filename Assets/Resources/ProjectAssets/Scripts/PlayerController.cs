using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject rightHand;
	public GameObject leftHand;
	EquipmentHandler equipmentHandler;
	HUD hud;
	private MouseLook _mouseLook;
	private FPSInputController _fpsController;
	private CharacterMotor _characterMotor;
		
	// Use this for initialization
	void Start () {
		equipmentHandler = gameObject.AddComponent<EquipmentHandler>();
		equipmentHandler.RightHand = rightHand;
		equipmentHandler.LeftHand = leftHand;
		hud = gameObject.AddComponent<HUD>();
		hud.AddQuickItem(0,(Equipment)Resources.Load ("ProjectAssets/prefabs/Flashlight", typeof(Equipment)));
		hud.AddQuickItem(1,(Equipment)Resources.Load ("ProjectAssets/prefabs/SprayCan", typeof(Equipment)));
		_mouseLook = gameObject.AddComponent<MouseLook> ();
		_characterMotor = gameObject.AddComponent<CharacterMotor> ();
		_fpsController = gameObject.AddComponent<FPSInputController> ();

	}

	// Update is called once per frame
	void Update () {
		if (Input.inputString.Length == 0)
			return;
		char keyPressed = Input.inputString [0];
		//update HUD
		if (char.IsDigit (keyPressed)) 
		{
			hud.SetQuickItemSelection((int)char.GetNumericValue (keyPressed));
			Equipment e = hud.GetQuickItemSelected();
			equipmentHandler.EquipLeft(e);
			//StartCoroutine("waitForEquip", keyPressed);
		}
		//update Equipment
	}

	public void waitForEquip(char keyPressed)
	{
		string keyName = "Quick" + keyPressed;
		//add ui showing that item is awaiting equip
		Equipment e = hud.GetQuickItemSelected();
		while (Input.GetButtonDown(keyName))
		{
			print ("here");
			if(Input.GetMouseButton(1))
			{
				equipmentHandler.EquipRight(e);
				return;
			}
			if(Input.GetMouseButton(0))
			{
				equipmentHandler.EquipLeft(e);
				return;
			}
		}


	}


	void FreezePlayer()
	{
		_mouseLook.enabled = false;
		_fpsController.enabled = false;
		_characterMotor.enabled = false;
		gameObject.GetComponent<CharacterController> ().enabled = false;
	}

	void UnfreezePlayer()
	{
		_mouseLook.enabled = true;
		_fpsController.enabled = true;
		_characterMotor.enabled = true;
		gameObject.GetComponent<CharacterController> ().enabled = true;
	}
}

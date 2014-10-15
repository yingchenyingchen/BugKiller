using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject rightHand;
	EquipmentHandler equipmentHandler;
	HUD2 hud;
	private MouseLook _mouseLook;
	private FPSInputController _fpsController;
	private CharacterMotor _characterMotor;
		
	// Use this for initialization
	void Start () {
		equipmentHandler = gameObject.AddComponent<EquipmentHandler>();
		equipmentHandler.RightHand = rightHand;
		hud = gameObject.AddComponent<HUD2>();
		hud.AddQuickItem((Equipment)Resources.Load ("ProjectAssets/prefabs/Flashlight", typeof(Equipment)));
		hud.AddQuickItem((Equipment)Resources.Load ("ProjectAssets/prefabs/SprayCan", typeof(Equipment)));
		_mouseLook = gameObject.AddComponent<MouseLook> ();
		_characterMotor = gameObject.AddComponent<CharacterMotor> ();
		_fpsController = gameObject.AddComponent<FPSInputController> ();

	}

	// Update is called once per frame
	void Update () {
		char keyPressed = Input.inputString [0];
		//update HUD
		if (char.IsDigit (keyPressed)) 
		{
			hud.SetQuickItemSelection((int)char.GetNumericValue (keyPressed));
			Equipment e = hud.GetQuickItemSelected();
			equipmentHandler.Equip(e);
		}
		//update Equipment
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

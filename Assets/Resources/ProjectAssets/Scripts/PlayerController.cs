using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject rightHand;
	public GameObject leftHand;
	public Equipment headObject;
	EquipmentHandler equipmentHandler;
	HUD hud;
	private MouseLook _mouseLook;
	private FPSInputController _fpsController;
	private CharacterMotor _characterMotor;
	private bool waitingOnEquip;
	private static KeyCode[] keyCodes;

		
	// Use this for initialization
	void Start () {
		if(keyCodes==null)
			keyCodes=(KeyCode[])System.Enum.GetValues(typeof(KeyCode));
		//quickslots/equiptment
		equipmentHandler = gameObject.AddComponent<EquipmentHandler>();
		equipmentHandler.RightHand = rightHand;
		equipmentHandler.LeftHand = leftHand;
		waitingOnEquip = false;
		//hud
		hud = gameObject.AddComponent<HUD>();
		hud.AddQuickItem(0,(Equipment)Resources.Load ("ProjectAssets/prefabs/Flashlight", typeof(Equipment)));
		hud.AddQuickItem(1,(Equipment)Resources.Load ("ProjectAssets/prefabs/SprayCan", typeof(Equipment)));
		//controllers
		_mouseLook = gameObject.AddComponent<MouseLook> ();
		_characterMotor = gameObject.AddComponent<CharacterMotor> ();
		_fpsController = gameObject.AddComponent<FPSInputController> ();

	}

	// Update is called once per frame
	void Update () {

		HandleHandEquip();

		if(Input.GetKeyDown("h"))
		{
			headObject.Activate();
		}
		//update Equipment
	}

	private void HandleHandEquip()
	{
		KeyCode code; 
		if (!waitingOnEquip && TryGetNumPressed(out code)) 
		{
			waitingOnEquip = true;
			string codeString = code.ToString();
			hud.SetQuickItemSelection((int)char.GetNumericValue (codeString[codeString.Length -1]));
			StartCoroutine(waitForEquip(code, hud.GetQuickItemSelected()));
		}
	}

	IEnumerator waitForEquip(KeyCode code, Equipment e)
	{
		//TODO: add ui showing that item is awaiting equip
		print (code);
		while (Input.GetKey(code))
		{
			print ("here");
			if(Input.GetMouseButton(1))
			{
				equipmentHandler.EquipRight(e);
				yield return true;
			}
			if(Input.GetMouseButton(0))
			{
				equipmentHandler.EquipLeft(e);
				yield return true;
			}
			yield return null;
		}
		waitingOnEquip = false;
		//TODO: remove ui element that indicates item is awaiting equip.
	}


	bool TryGetNumPressed(out KeyCode code)
	{
		for (int i = 0; i < 10; i ++) 
		{
			string keyName = "Alpha" + i;
			KeyCode tempCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + i);
			if(Input.GetKeyDown(tempCode))
			{
				code = tempCode;
				return true;
			}
		}
		code = KeyCode.None;
		return false;
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

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
	private RaycastHit whichBugsHit;
	public Equipment whichEquipment;
	public bool rightClicked;

		
	// Use this for initialization
	void Start () {
		if(keyCodes==null)
			keyCodes=(KeyCode[])System.Enum.GetValues(typeof(KeyCode));
		var RH = (Equipment)Resources.Load ("ProjectAssets/prefabs/RHand", typeof(Equipment));
		var LH = (Equipment)Resources.Load ("ProjectAssets/prefabs/LHand", typeof(Equipment));
		//quickslots/equiptment
		equipmentHandler = gameObject.AddComponent<EquipmentHandler>();
		equipmentHandler.RightHand = rightHand;
		equipmentHandler.LeftHand = leftHand;
		equipmentHandler.HandModelLeft = LH;
		equipmentHandler.HandModelRight = RH;
		waitingOnEquip = false;
		//hud
		hud = gameObject.AddComponent<HUD>();
		hud.AddQuickItem(0,(Equipment)Resources.Load ("ProjectAssets/prefabs/Flashlight", typeof(Equipment)));
		hud.AddQuickItem(1,(Equipment)Resources.Load ("ProjectAssets/prefabs/SprayCan", typeof(Equipment)));
		hud.AddQuickItem(2, LH);
		hud.AddQuickItem(3, RH);
		hud.AddQuickItem(4,(Equipment)Resources.Load ("ProjectAssets/prefabs/Aerosol_Can_2", typeof(Equipment)));
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
			headObject.Activate(KeyCode.H);
		}
		//update Equipment
		KillBugs ();
	}

	//Kill bugs when certain keys are clicked. 
	void KillBugs(){
		
		//var rotSpeed = 60; 
		//Vector3 v3 = new Vector3 (0,90,0);
		Color c = new Color(0.25f,1.41f,1.78f);
		//whichBugshit
		rightClicked = Input.GetMouseButton (1);
		if((whichEquipment is SprayCan) && rightClicked )
		{
			//if(Physics.SphereCast (transform.position, 400f, transform.forward, out whichBugsHit, Mathf.Infinity, 1<< LayerMask.NameToLayer("Bugs")))
			//int b = Physics.SphereCastAll (transform.position, 800f, transform.forward, 800f, 1<< LayerMask.NameToLayer("Bugs")).Length;
			
			if(Physics.SphereCast (transform.position, 5f, transform.forward,out whichBugsHit, 3f, 1<< LayerMask.NameToLayer("Bugs"))){
				//if(b!=0){
				GameObject go = whichBugsHit.collider.gameObject; //transform.Rotate(v3);
				//go.transform.Rotate(v3);
				go.renderer.material.color = c;
				Destroy(whichBugsHit.collider.gameObject, 5f);
				
			}
		}
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
			whichEquipment = hud.GetQuickItemSelected();
		}
	}

	IEnumerator waitForEquip(KeyCode code, Equipment e)
	{
		//TODO: add ui showing that item is awaiting equip
		print (code);
		while (Input.GetKey(code))
		{
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

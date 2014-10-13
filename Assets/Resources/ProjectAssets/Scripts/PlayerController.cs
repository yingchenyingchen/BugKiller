using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject rightHand;
	EquiptmentHandler equiptmentHandler;
	HUD hud;
		
	// Use this for initialization
	void Start () {
		equiptmentHandler = gameObject.AddComponent<EquiptmentHandler>();
		equiptmentHandler.RightHand = rightHand;
		hud = gameObject.AddComponent<HUD>();
		//Equiptment e = (Equiptment)Instantiate (Resources.Load ("ProjectAssets/prefabs/Flashlight", typeof(Equiptment)));
		hud.AddQuickItem((Equiptment)Resources.Load ("ProjectAssets/prefabs/Flashlight", typeof(Equiptment)));
		hud.AddQuickItem((Equiptment)Resources.Load ("ProjectAssets/prefabs/SprayCan", typeof(Equiptment)));

	}

	// Update is called once per frame
	void Update () {
		char keyPressed = Input.inputString [0];
		//update HUD
		if (char.IsDigit (keyPressed)) 
		{
			hud.SetQuickItemSelection((int)char.GetNumericValue (keyPressed));
			Equiptment e = hud.GetQuickItemSelected();
			equiptmentHandler.Equip(e);
		}
		//update Equiptment
	}
}

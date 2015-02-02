using UnityEngine;
using System.Collections;

public class ObjectGrab : MonoBehaviour {
	private bool handsFull = false;
	Rigidbody objectHeld;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonUp (0))
		{
			if(handsFull)
				dropObjects ();
			return;
		}
		
		if (!Input.GetMouseButtonDown (0))
			return;
		
		RaycastHit hit = new RaycastHit ();
		
		if (!Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 500)) {
			return;
		}
		Rigidbody body = hit.rigidbody;
		if (!body || body.isKinematic) {
			return;
		}
		
		grabObject (body);
	}
	
	void grabObject(Rigidbody body)
	{
		objectHeld = body;
		handsFull = true;
		body.useGravity = false;
		body.transform.parent = Camera.main.transform;
		body.isKinematic = true;
	}
	
	void dropObjects()
	{
		print ("DROP");
		handsFull = false;
		objectHeld.useGravity = true;
		objectHeld.transform.parent = null;
		objectHeld.isKinematic = false;
	}
}

using UnityEngine;
using System.Collections;

public class EmptyHand : Equipment {

	public bool attachToCenterOfMass;
	public double liftWeightLimit;
	public float jointBreakPoint; 
	private bool _handFull = false;
	private FixedJoint joint;
	private string _activateButton;
	private GameObject _holdAnchorTemplate;
	private GameObject _holder;
	private KeyCode _input;


	public EmptyHand ()
	{
		liftWeightLimit = 1000;
		attachToCenterOfMass = false;
		jointBreakPoint = 1000f;
		_handFull = false;
		_holdAnchorTemplate = (GameObject)Resources.Load ("ProjectAssets/prefabs/grabAnchor", typeof(GameObject));
	}

	// Use this for initialization
	void Start () {
		Equipped = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckForDrop ();
	}

	//pick stuff up
	public override void Activate(KeyCode input)
	{
		_input = input;
		if (_handFull) 
		{
			dropObjects ();
			return;
		}
		
		RaycastHit hit = new RaycastHit ();

		if (!Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 500)) {
			return;
		}
		
		Rigidbody body = hit.rigidbody;
		
		// We need to hit a rigidbody that is not kinematic
		if (!body || body.isKinematic) {
			return;
		}
		//we need some way to ensure that we do not try to pick up very heavy objects.  Should we simply choose not to tag such objects as grabbable?
		if (body.mass > liftWeightLimit) {
			return;
		}

		grabObject (hit.point, body);
	}

	private void CheckForDrop()
	{
		if (!Input.GetKey (_input))
		{
			if(_handFull)
				dropObjects ();
			return;
		}
	}

	void dropObjects()
	{
		_handFull = false;
		foreach (Component grabberJoint in _holder.GetComponents<FixedJoint>())
			Destroy (grabberJoint);
		Destroy (_holder);
	}

	void grabObject(Vector3 hitPoint, Rigidbody body)
	{
		_holder = (GameObject)Instantiate(_holdAnchorTemplate);
		_holder.transform.position = gameObject.transform.position;
		_holder.transform.rotation = gameObject.transform.rotation;
		_holder.transform.parent = gameObject.transform;

		_handFull = true;
		if (!joint)
		{
			addJoint ();
		}
		joint.transform.position = hitPoint;
		
		setAnchorPoint (body);
		joint.connectedBody = body;
	}


	void setAnchorPoint(Rigidbody body)
	{
		if (attachToCenterOfMass)
		{
			var anchor = transform.TransformDirection(body.centerOfMass) + body.transform.position;
			anchor = joint.transform.InverseTransformPoint(anchor);
			joint.anchor = anchor;
		}
		else
		{
			joint.anchor = Vector3.zero;
		}
	}

	void addJoint() 
	{
		joint = (FixedJoint) _holder.AddComponent ("FixedJoint");
		joint.breakForce = jointBreakPoint;
	}

	public override void OnEquip()
	{
		Equipped = true;
	}
	
	public override void OnUnequip()
	{
		dropObjects();
	}
}

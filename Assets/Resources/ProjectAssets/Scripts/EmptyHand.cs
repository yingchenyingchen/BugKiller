using UnityEngine;
using System.Collections;

public class EmptyHand : Equipment {

	public bool attachToCenterOfMass = false;
	public double liftWeightLimit = 1000;
	public float jointBreakPoint; 
	private bool _handFull = false;
	private FixedJoint joint;
	private string _activateButton;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckForDrop ();
	}

	//pick stuff up
	public override void Activate()
	{
		if (_handFull) 
		{
			dropObjects ();
			return;
		}
		
		RaycastHit hit = new RaycastHit ();

		if (!Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 500)) {
			print ("a");
			return;
		}
		
		Rigidbody body = hit.rigidbody;
		
		// We need to hit a rigidbody that is not kinematic
		if (!body || body.isKinematic) {
			print ("b");
			return;
		}
		//we need some way to ensure that we do not try to pick up very heavy objects.  Should we simply choose not to tag such objects as grabbable?
		if (body.mass > liftWeightLimit) {
			print ("c");
			return;
		}

		grabObject (hit.point, body);
	}

	private void CheckForDrop()
	{
		if (!Input.GetMouseButton (0))
		{
			if(_handFull)
				dropObjects ();
			return;
		}
	}

	void dropObjects()
	{
		_handFull = false;
		foreach (Component grabberJoint in gameObject.GetComponents<FixedJoint>())
			Destroy (grabberJoint);
	}

	void grabObject(Vector3 hitPoint, Rigidbody body)
	{
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
		joint = (FixedJoint) gameObject.AddComponent ("FixedJoint");
		joint.breakForce = jointBreakPoint; //arbitrary
	}
}

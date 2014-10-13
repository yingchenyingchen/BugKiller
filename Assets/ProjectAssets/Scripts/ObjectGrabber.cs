using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectGrabber : MonoBehaviour {	
	public bool attachToCenterOfMass = false;
	public GameObject grabber;
	public double liftWeightLimit;
	public double dragWeightLimit;
	public float jointBreakPoint; 
	private FixedJoint joint;
	private bool handsFull = false;

	void Update () 
	{
		if (!Input.GetMouseButton (0))
		{
			if(handsFull)
				dropObjects ();
			return;
		}

		if (handsFull)
			return;
		
		RaycastHit hit = new RaycastHit ();
		
		if (!Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 500)) {
						print ("a");
						return;
				}

		Rigidbody body = hit.rigidbody;

		// We need to hit a rigidbody that is not kinematic and is tagged as 'Grabbable"
		if (!body || body.isKinematic) {
						print ("b");
						return;
				}
		//we need some way to ensure that we do not try to pick up very heavy objects.  Should we simply choose not to tag such objects as grabbable?
		if (body.mass > liftWeightLimit) {
						print ("c");
						return;
				}

		if (playerStandingOn(body))
		{
		    dropObjects();
		    return;
		}
		
		grabObject (hit.point, body);
	}

	void addJoint() 
	{
		joint = (FixedJoint) grabber.AddComponent ("FixedJoint");
		joint.breakForce = jointBreakPoint; //arbitrary
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

	void dropObjects()
	{
		print ("drop");
		handsFull = false;
		foreach (Component grabberJoint in grabber.GetComponents<FixedJoint>())
						Destroy (grabberJoint);

	}

	void grabObject(Vector3 hitPoint, Rigidbody body)
	{
		handsFull = true;
		if (!joint)
		{
			addJoint ();
		}
		joint.transform.position = hitPoint;
		
		setAnchorPoint (body);
		joint.connectedBody = body;
	}


	bool playerStandingOn(Rigidbody body)
	{
		RaycastHit hit = new RaycastHit ();
		return Physics.Raycast (transform.position, -Vector3.up,out hit, 5)  && hit.rigidbody == body;
	}


}

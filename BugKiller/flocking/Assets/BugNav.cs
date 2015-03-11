using UnityEngine;
using System.Collections;

public class BugNav : MonoBehaviour {

	RaycastHit forward;
	public float _senseDownDistance;
	public GameObject Sensor;
	public float lerpSpeed;

	// Use this for initialization
	void Start () {
		//down = new RaycastHit ();
		forward = new RaycastHit ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit down = new RaycastHit ();
		Ray ray = new Ray (Sensor.transform.position, Vector3.down);
		if (Physics.Raycast(ray, out down, _senseDownDistance ))
		    MoveTo (down);
	}

	void MoveTo(RaycastHit hit)
	{
		Vector3 target = hit.transform.position;
		Vector3 direction = GetDirectionTo (target);
		float ydiff = transform.position.y - direction.y;
		gameObject.rigidbody.velocity = new Vector3(direction.x, 0, direction.z) * 10;

		Vector3 myNormal = transform.up;
		Vector3 surfaceNormal = hit.normal;
		//lerp between cube normal and sphere surface normal
		myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed*Time.deltaTime);
		//find the target forward direction
		Vector3 myForward = Vector3.Cross(transform.right, myNormal);
		//get quaternion target rotation
		Quaternion targetRotation = Quaternion.LookRotation(myForward, myNormal);
		//rotate cube
		Quaternion q = Quaternion.FromToRotation(myNormal, surfaceNormal);
		q = q*transform.rotation;
		transform.rotation = Quaternion.Slerp(transform.rotation, q, lerpSpeed*Time.deltaTime);
	}

	Vector3 GetDirectionTo(Vector3 target)
	{
		Vector3 desired = target - transform.position;
		desired.Normalize ();
		return desired;
	}
}

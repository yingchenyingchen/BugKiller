using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BugAI : MonoBehaviour {
	private List<GameObject> _attractants;
	public float attractantWeight;
	public float attractantRadius;
	public float maxSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_attractants = getAttractants ();
		Vector3 flock = Flock ();
		Vector3 acceleration = new Vector3 (flock.x, 0, flock.z);
		if((rigidbody.velocity + acceleration).magnitude > maxSpeed){
			Vector3 temp = (rigidbody.velocity + acceleration);
			temp.Normalize();
			temp *= maxSpeed;
			rigidbody.velocity = temp;
		}
		else
			rigidbody.velocity = acceleration;
	}

	List<GameObject> getAttractants(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, attractantRadius);
		List<GameObject> objects = new List<GameObject> ();
		foreach (Collider c in colliders) {
			if(c.gameObject.CompareTag("Attractant"))
				break;
			objects.Add(c.gameObject);
		}
		return objects;
	}

	Vector3 Flock()
	{
		var eat = GetAverageDirectionToAttractants () * attractantWeight; 
		return eat;
	}

	Vector3 GetAverageDirectionToAttractants(){
		Vector3 sum = new Vector3 (0,0,0);
		int count = 0;
		foreach (GameObject attractant in _attractants) {
			sum += attractant.transform.position;
			count ++;
		}
		if (count > 0)
			return SteerTo (sum / count);
		return sum;
	}
	
	Vector3 SteerTo(Vector3 target){
		Vector3 desired = target - gameObject.transform.position;
		Vector3 steer = new Vector3 (0, 0, 0);
		float distance = desired.magnitude;
		if (distance > 0)
		{
			desired.Normalize ();
			if(distance < 100)
				desired *= (maxSpeed *(distance/100));
			else desired *= maxSpeed;
			steer =  desired - gameObject.rigidbody.velocity;
			//limit to maxForce
		}
		return steer;
	}
}

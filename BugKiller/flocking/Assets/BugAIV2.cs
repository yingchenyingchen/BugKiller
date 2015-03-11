using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BugAIV2 : MonoBehaviour {
	private List<GameObject> _attractants;
	private List<GameObject> _deterrents;
	public bool canFly;
	public float attractantRadius;
	public float speed;

	// Use this for initialization
	void Start () {
		renderer.material.SetColor ("_Color", Color.black);

		_attractants = new List<GameObject> ();
		_deterrents = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		getStimuli ();
		Vector3 direction = GetAverageStimuliDirection ();
		if (canFly)
			moveAnywhere (direction);
		else
			moveOnGround (direction);
	}

	void moveOnGround(Vector3 direction){
		gameObject.rigidbody.velocity = new Vector3 (direction.x, gameObject.rigidbody.velocity.y, direction.z) * speed;
	}

	void moveAnywhere(Vector3 direction){
		gameObject.rigidbody.velocity = direction;
	}

	void getStimuli(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, attractantRadius);
		foreach (Collider c in colliders) {
			if((c.gameObject.GetComponent("Attractant") as Attractant) != null)
				_attractants.Add(c.gameObject);
			else if(c.gameObject.GetComponent("Repellant") as Repellant != null)
				_deterrents.Add(c.gameObject);
		}
	}


	
	Vector3 GetAverageStimuliDirection(){
		Vector3 sum = new Vector3 (0, 0, 0);
		if (_attractants.Count == 0 && _deterrents.Count == 0)
			return sum;
		//for each attraact, add the vector from you to it.
		foreach (GameObject attractant in _attractants) {
			Vector3 temp = attractant.transform.position - gameObject.transform.position;;
			temp = Vector3.ClampMagnitude(temp, speed/temp.magnitude);
			temp *= (attractant.GetComponent("Attractant") as Attractant).strength;
			sum += temp;
		}
		//for each deterrent, add the vector away from it.
		foreach (GameObject deterrent in _deterrents) {
			Vector3 temp = deterrent.transform.position - gameObject.transform.position;;
			temp = Vector3.ClampMagnitude(temp, speed/temp.magnitude);
			temp *= (deterrent.GetComponent("Repellant") as Repellant).strength;
			sum -= temp;
		}
		sum.Normalize();
		return sum;
	}
	

	Vector3 GetDirectionTo(Vector3 target)
	{
		Vector3 desired = target - gameObject.transform.position;
		desired.Normalize ();
		return desired;
	}
}


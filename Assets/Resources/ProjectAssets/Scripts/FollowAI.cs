using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowAI : MonoBehaviour {
	private List<GameObject> _attractants;
	public bool canFly;
	public float attractantRadius;
	public float speed;
	
	// Use this for initialization
	void Start () {
		renderer.material.SetColor ("_Color", Color.black);
		
		_attractants = new List<GameObject> ();

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
			if (c.gameObject.CompareTag ("Player"))
				_attractants.Add (c.gameObject);
		}
	}
	
	
	
	Vector3 GetAverageStimuliDirection(){
		Vector3 sum = new Vector3 (0, 0, 0);
		if (_attractants.Count == 0)
			return sum;
		//for each attraact, add the vector from you to it.
		foreach (GameObject attractant in _attractants) {
			Vector3 temp = attractant.transform.position - gameObject.transform.position;;
			temp = Vector3.ClampMagnitude(temp, attractantRadius/temp.magnitude);
			sum += temp;
		}
		//for each deterrent, add the vector away from it.
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


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CockroachAI : MonoBehaviour {
	private List<GameObject> _attractants;
	private List<GameObject> _deterrents;
	public bool canFly;
	public float attractantRadius;
	public float speed;

	private NavMeshAgent agent;
	
	// Use this for initialization
	void Start () {
		renderer.material.SetColor ("_Color", Color.black);
		
		_attractants = new List<GameObject> ();
		_deterrents = new List<GameObject> ();

		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		getStimuli ();
		Vector3 direction = GetAverageStimuliDirection ();

		if (canFly) {
						moveAnywhere (direction);
				} else {
						moveOnGround (direction);
				}
			//Vector3 vvv = new Vector3 (-0.2f, 0.0f, -0.2f);
			//moveOnGround (direction);
			//transform.position = new Vector3(Mathf.Lerp(23.1f, 28.5f, Time.time), 7.7f, 2.5f);
			//Debug.Log(Mathf.Lerp(0.1f, 0.5f, Time.time));
	}
	
	void moveOnGround(Vector3 direction){
		//gameObject.rigidbody.velocity = new Vector3 (direction.x, gameObject.rigidbody.velocity.y, direction.z) * speed;
		agent.SetDestination (new Vector3 (direction.x, gameObject.rigidbody.velocity.y, direction.z) * speed);
		//gameObject.rigidbody.angularVelocity
	}
	
	void moveAnywhere(Vector3 direction){
		gameObject.rigidbody.velocity = direction;
	}
	
	void getStimuli(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, attractantRadius);
		foreach (Collider c in colliders) {
			if (c.gameObject.CompareTag ("FoodAttractant"))
				_attractants.Add (c.gameObject);
			else if (c.gameObject.CompareTag ("Deterrent"))
				_deterrents.Add (c.gameObject);
		}
	}
	
	
	
	Vector3 GetAverageStimuliDirection(){
		Vector3 sum = new Vector3 (0, 0, 0);
		if (_attractants.Count == 0 && _deterrents.Count == 0)
			return sum;
		//for each attraact, add the vector from you to it.
		foreach (GameObject attractant in _attractants) {
			Vector3 temp = attractant.transform.position - gameObject.transform.position;;
			temp = Vector3.ClampMagnitude(temp, attractantRadius/temp.magnitude);
			sum += temp;
		}
		//for each deterrent, add the vector away from it.
		foreach (GameObject deterrent in _deterrents) {
			Vector3 temp = deterrent.transform.position - gameObject.transform.position;;
			temp = Vector3.ClampMagnitude(temp, attractantRadius/temp.magnitude);
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


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FlockWithBaitBehaviour : MonoBehaviour {
	
	private List<GameObject>  _neighbors;
	private List<Repellant> _repellants;
	private List<Attractant> _attractants;
	public float neighborRadius;
	public int maxSpeed;
	public int maxDistance;
	public float separationWeight;
	public float alignmentWeight;
	public float cohesionWeight;
	public float desiredSeparation;
	public float repellantWeight;
	public float attractantWeight;
	public GameObject LocalOrigin;
	
	// Use this for initialization
	void Start () {
		rigidbody.velocity = new Vector3 (Random.value * 10, Random.value * 10, Random.value * 10);
	}
	
	// Update is called once per frame
	void Update () {
		_neighbors = getNeighbors ();
		_repellants = getRepellants ();
		_attractants = getAttractants ();
		Vector3 acceleration = Flock ();
		if((rigidbody.velocity + acceleration).magnitude > maxSpeed){
			Vector3 temp = (rigidbody.velocity + acceleration);
			temp.Normalize();
			temp *= maxSpeed;
			rigidbody.velocity = temp;
		}
		else
			rigidbody.velocity += acceleration;
		if (Vector3.Distance (LocalOrigin.transform.position, transform.position) > maxDistance) {
			Vector3 dir = LocalOrigin.transform.position - transform.position;
			dir.Normalize();
			rigidbody.AddForce(dir * 100);
		}

	}
	
	List<GameObject> getNeighbors(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, neighborRadius);
		List<GameObject> objects = new List<GameObject> ();
		foreach (Collider c in colliders) {
			GameObject temp = c.gameObject;
			if (temp.GetComponent("Rigidbody") == null)
				break;
			if((temp.GetComponent<FlockWithBaitBehaviour>() as FlockWithBaitBehaviour) == null)
				break;
			objects.Add(c.gameObject);
		}
		return objects;
	}
	
	List<Repellant> getRepellants(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, neighborRadius);
		List<Repellant> objects = new List<Repellant> ();
		foreach (Collider c in colliders) {
			GameObject temp = c.gameObject;
			if(temp.GetComponent<Repellant>() == null)
				break;
			objects.Add(temp.GetComponent<Repellant>());
		}
		return objects;
	}

	List<Attractant> getAttractants(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, neighborRadius);
		List<Attractant> objects = new List<Attractant> ();
		foreach (Collider c in colliders) {
			GameObject temp = c.gameObject;
			if(temp.GetComponent<Attractant>() == null)
				break;
			objects.Add(temp.GetComponent<Attractant>());
		}
		return objects;
	}
	
	Vector3 Flock()
	{
		var separation = Separate() * separationWeight;
		var alignment = Align() * alignmentWeight;
		var cohesion = Cohere() * cohesionWeight;
		var flee = RunAway () * repellantWeight;
		var eat = Eat () * attractantWeight; 
		return separation + alignment + cohesion + flee + eat;
	}
	
	Vector3 Cohere ()
	{
		Vector3 sum = new Vector3 (0,0,0);
		int count = 0;
		foreach (GameObject neighbor in _neighbors) 
		{
			float distance = Vector3.Distance (neighbor.transform.position, gameObject.transform.position);
			if (distance > 0 && distance < neighborRadius){
				sum += neighbor.transform.position;
			}
		}
		if (count > 0)
			return SteerTo (sum / count);
		return sum;
	}

	Vector3 RunAway (){
		Vector3 sum = new Vector3 (0,0,0);
		int count = 0;
		foreach (Repellant repellant in _repellants) {
			sum += repellant.transform.position * repellant.strength;
			count ++;
		}
		if (count > 0)
			return SteerTo (sum / count);
		return sum;
	}

	Vector3 Eat(){
		Vector3 sum = new Vector3 (0,0,0);
		int count = 0;
		foreach (Attractant attractant in _attractants) {
			sum += attractant.transform.position * attractant.strength;
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
			steer = desired - gameObject.rigidbody.velocity;
			//limit to maxForce
		}
		return steer;
	}
	
	Vector3 Align(){
		Vector3 mean = new Vector3 (0, 0, 0);
		int count = 0;
		foreach (GameObject neighbor in _neighbors){
			float distance =Vector3.Distance(gameObject.transform.position, neighbor.transform.position);
			if (distance > 0 && distance < neighborRadius){
				mean += neighbor.rigidbody.velocity;
				count++;
			}
		}
		if (count > 0)
			mean = mean/count;
		//Limit Mean by maxForce
		return mean;
	}
	
	Vector3 Separate(){
		Vector3 mean = new Vector3 (0, 0, 0);
		int count = 0;
		foreach (GameObject neighbor in _neighbors){
			float distance = Vector3.Distance(gameObject.transform.position, neighbor.transform.position);
			if (distance > 0  && distance < desiredSeparation){
				Vector3 temp = (transform.position - neighbor.transform.position);
				temp.Normalize();
				temp = temp/distance;
				mean += temp;
				count++;
			}		
		}
		if (count > 0)
			mean = mean / count;
		return mean;
	}
}


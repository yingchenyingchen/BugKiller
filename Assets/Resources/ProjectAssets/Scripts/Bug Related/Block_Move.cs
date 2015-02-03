/// <summary>
/// Part of Johnothan's code toward the end. 
/// </summary>
using UnityEngine;
using System.Collections;

public class Block_Move : MonoBehaviour {
	
	public GameObject otherBlock;
	Vector3 vector;
	public float maxDistance;
	
	// Use this for initialization
	void Start () {
		//otherBlock = GameObject.FindWithTag("BlockToOrbitAround");
	}
	
	// Update is called once per frame
	void Update () {
		// moves the block up and forward
		//transform.Translate (Vector3.forward * Time.deltaTime);
		//transform.Translate(Vector3.up * Time.deltaTime, Space.World);
		vector = otherBlock.transform.position - gameObject.transform.position;
		if (vector.magnitude > maxDistance)
			transform.LookAt (otherBlock.transform.localPosition);
		else {
			if (Random.value < 0.6)	
				transform.Rotate (0, Random.Range (-15, 25), 0);
		}
		transform.Translate (Vector3.forward * Time.deltaTime);
		
		//transform.RotateAround(otherBlock.transform.position, Vector3.up, 20 * Time.deltaTime);
	}
}

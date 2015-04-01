using UnityEngine;
using System.Collections;

public class ReverseGravity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("r"))
						Physics.gravity = -Physics.gravity;

	}
}

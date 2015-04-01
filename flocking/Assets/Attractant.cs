using UnityEngine;
using System.Collections;

public class Attractant : MonoBehaviour {

	public float strength;

	// Use this for initialization
	void Start () {
		renderer.material.SetColor ("_Color", Color.green);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

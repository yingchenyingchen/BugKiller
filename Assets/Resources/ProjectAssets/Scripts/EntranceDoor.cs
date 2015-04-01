using UnityEngine;
using System.Collections;

public class EntranceDoor : MonoBehaviour {

	bool open;
	double zone;
	float distance;
	int doorRotation;
	GameObject player;
	//in Unity3D, gameObject refers to the GameObject the script is attached to.
	
	// Use this for initialization
	void Start ()
	{
		open = false;
		zone = 2.5;
		distance = 100;
		doorRotation = 90;
		player = GameObject.FindGameObjectsWithTag("Deterrent")[0];
	}
	
	// Update is called once per frame
	void Update ()
	{
		distance = Vector3.Distance(transform.position, player.transform.position);
		//if player is within the distance
		if(distance < zone)
		{
			//when the player is near and e is pressed
			if (Input.GetKeyDown(KeyCode.E))
			{
				doorRotation *= -1;
				open = !open;
				gameObject.transform.Rotate(new Vector3(0, doorRotation, 0));
			}
		}
	}
}

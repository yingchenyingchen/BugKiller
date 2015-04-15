using UnityEngine;
using System.Collections;

public class Lightswitch : MonoBehaviour {

	public string light_name;
	bool on;
	double zone;
	float distance;
	GameObject player;
	GameObject light;
	Light intensity;
	//in Unity3D, gameObject refers to the GameObject the script is attached to.
	
	// Use this for initialization
	void Start ()
	{
		on = false;
		zone = 2.5;
		distance = 100;
		player = GameObject.FindGameObjectsWithTag("Deterrent")[0];
		light = GameObject.Find(light_name);
		intensity = light.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		distance = Vector3.Distance(transform.position, player.transform.position);
		//if player is within the distance
		if(distance < zone)
		{
			//when the player is near and e is pressed
			if (Input.GetKeyDown(KeyCode.Z))
			{
				intensity.intensity = on ? 0f : 1.5f;
				on = !on;
				gameObject.transform.Rotate(new Vector3 (180f, 0f, 0f));
			}
		}
	}
}

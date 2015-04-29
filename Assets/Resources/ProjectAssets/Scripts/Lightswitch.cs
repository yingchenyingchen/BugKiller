using UnityEngine;
using System.Collections;

public class Lightswitch : MonoBehaviour {

	public string light_name;
	bool on;
	double zone;
	float distance;
	GameObject player;
	string[] lights;
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
		lights = light_name.Split(',');
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
				float brightness = (lights.Length > 1) ? 2.5f : 1.5f;
				foreach (string str in lights)
				{
					light = GameObject.Find(str);
					intensity = light.GetComponent<Light>();
					intensity.intensity = on ? 0f : brightness;
				}
				on = !on;
				gameObject.transform.Rotate(new Vector3 (180f, 0f, 0f));
			}
		}
	}
}

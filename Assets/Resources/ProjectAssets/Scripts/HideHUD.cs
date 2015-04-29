using UnityEngine;
using System.Collections;

public class HideHUD : MonoBehaviour {

	float x;
	float y;
	float z;
	float startx;
	float starty;
	float startz;

	// Use this for initialization
	void Start ()
	{
		Vector3 v3 = gameObject.transform.position;
		startx = v3.x;
		starty = v3.y;
		startz = v3.z;
		x = startx + 150;
		y = starty - 150;
		z = startz;
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool down = Input.GetKeyDown(KeyCode.Q);
		bool held = Input.GetKey(KeyCode.Q);
		bool up = Input.GetKeyUp(KeyCode.Q);

		if (down)
		{
			gameObject.transform.position = new Vector3(startx, starty, startz);
		}
		else if (held)
		{
			gameObject.transform.position = new Vector3(startx, starty, startz);
		}
		else if (up)
		{
			gameObject.transform.position = new Vector3(x, y, z);
		}
		else
		{
			gameObject.transform.position = new Vector3(x, y, z);
		}
	}
}

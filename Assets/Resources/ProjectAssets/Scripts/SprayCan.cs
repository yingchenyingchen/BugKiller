using UnityEngine;
using System.Collections;

public class SprayCan :  Equiptment {
	void Start ()
	{
		if (!gameObject.particleSystem)
						gameObject.AddComponent ("ParticleSystem");
	}
	
	
	// Update is called once per frame
	void Update () {

		 if (!Input.GetMouseButton(0))
		{
			gameObject.particleSystem.enableEmission = false;
			gameObject.particleSystem.Stop();
		}
	}

	public override void Activate()
	{
		gameObject.particleSystem.enableEmission = true;
		gameObject.particleSystem.Play();
	}
}

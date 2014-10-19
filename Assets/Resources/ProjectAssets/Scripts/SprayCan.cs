using UnityEngine;
using System.Collections;

public class SprayCan :  Equipment {

	public GameObject SprayNozzle;
	void Start ()
	{
		if (!SprayNozzle.particleSystem)
			SprayNozzle.AddComponent ("ParticleSystem");
	}
	
	
	// Update is called once per frame
	void Update () {

		 if (!Input.GetMouseButton(0))
		{
			SprayNozzle.particleSystem.enableEmission = false;
			SprayNozzle.particleSystem.Stop();
		}
	}

	public override void Activate()
	{
		SprayNozzle.particleSystem.enableEmission = true;
		SprayNozzle.particleSystem.Play();
	}
}

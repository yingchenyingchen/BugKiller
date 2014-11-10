using UnityEngine;
using System.Collections;

public class SprayCan :  Equipment {

	public GameObject SprayNozzle;
	private int mouseNum;

	void Start ()
	{
		Equipped = false;
		if (!SprayNozzle.particleSystem)
			SprayNozzle.AddComponent ("ParticleSystem");
	}
	
	
	// Update is called once per frame
	void Update () {

		 if (!Input.GetMouseButton(mouseNum))
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

	public void Activate (int mouseNum)
	{
		SprayNozzle.particleSystem.enableEmission = true;
		SprayNozzle.particleSystem.Play();
	}

	public override void OnEquip()
	{
		Equipped = true;
	}
	
	public override void OnUnequip()
	{
		
	}
}

using UnityEngine;
using System.Collections;

public class AerosolCan :  Equipment {
	
	public GameObject SprayNozzle;
	private int mouseNum;
	private KeyCode _input;
	
	void Start ()
	{
		Equipped = false;
		if (!SprayNozzle.particleSystem)
			SprayNozzle.AddComponent ("ParticleSystem");
	}
	
	
	// Update is called once per frame
	void Update () {
		
		if (!Input.GetKey(_input))
		{
			SprayNozzle.particleSystem.enableEmission = false;
			SprayNozzle.particleSystem.Stop();
		}
	}
	
	
	public override void Activate (KeyCode input)
	{
		_input = input;
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

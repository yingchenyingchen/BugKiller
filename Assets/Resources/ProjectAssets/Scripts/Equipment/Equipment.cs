using UnityEngine;
using System.Collections;

public abstract class Equipment : MonoBehaviour {

	public bool Equipped{ get; set; }

	public abstract void Activate(KeyCode input);

	public abstract void OnEquip();

	public abstract void OnUnequip();

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuickInventory {
	private Equipment _defaultItem = ((Equipment)Resources.Load ("ProjectAssets/prefabs/Grabber", typeof(Equipment)));

	private List<Equipment> _equipmentList;

	public int Count{ get; set; }

	public QuickInventory(int count)
	{
		_equipmentList = new List<Equipment> (count);
		for (int i = 0; i<count; i++)
			_equipmentList.Add (_defaultItem);
	}

	public void SetItem(int i, Equipment e)
	{
		_equipmentList [i] = e;
	}

	public void ExpandList(int count)
	{
		for (; count > 0; count --)
			ExpandList ();
	}

	public void ExpandList()
	{
		_equipmentList.Add (_defaultItem);
	}

	public Equipment this[int index]
	{
		get{return _equipmentList[index];}
		set{ _equipmentList [index] = value;}
	}

}

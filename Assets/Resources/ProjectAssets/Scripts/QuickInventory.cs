using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuickInventory {
	private Equipment _defaultItem;

	private List<Equipment> _equipmentList;

	public QuickInventory(int count, Equipment defaultItem)
	{
		_defaultItem = defaultItem;
		_equipmentList = new List<Equipment> (count);
		for (int i = 0; i<count; i++)
			_equipmentList.Add (_defaultItem);
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



}

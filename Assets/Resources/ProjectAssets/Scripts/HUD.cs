//NEEDS CLEANING:  Hud quick select slots should be data-bound to underlying quickInventory data structure. Quick inventory should be made into
//custom data structure which autofills any 'empty' slots with our default 'empty hand' item.  This 'empty hand' item should contain the 'carry object'
//and 'grab object' scripts!  Without and empty hand, our character cannot grab things!

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {

	public List<Equipment> quickInventory = new List<Equipment> ();
	public Equipment e1;
	public Equipment e2;
	
	//Menu Variables
	private bool MenuOpen = false;
	int MenuWidthStart = (Screen.width/2)-75;
	int MenuHeightStart = (Screen.height/2)-125;
	int MenuSpacing = 30;
	
	//Menu Inventory Variables
	bool InventoryOpen = false;
	int InventoryWidthStart = (Screen.width / 2) - 250;
	int InventoryHeightStart = (Screen.height / 2) - 250;
	int InventoryWidth = 500;
	int InventoryHeight = 500;
	
	//Quick Inventory Variables
	bool qInventoryShown = true;
	int selectionGridInt = 0;
	string[] selectionStrings = {"Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6"};

	void Start()
	{
		AddQuickItem (e1);
		AddQuickItem (e2);
		print (quickInventory.Count);
	}

	void Update() 
	{
		//Screen.lockCursor = true;
		updateQuickItemGui ();
	}

	void OnGUI(){
		GUI.Box (new Rect (Screen.width-200,0,200,25), "Air Quality Bar");
		GUI.Box (new Rect (Screen.width-200,30,200,25), "% Bugs Found");
		
		if (qInventoryShown) {
			selectionGridInt = GUI.SelectionGrid (new Rect (Screen.width / 2 - 250, Screen.height - 55, 500, 50), selectionGridInt, selectionStrings, 6);
		}
		
		if (InventoryOpen) {
			GUI.Box (new Rect (InventoryWidthStart,InventoryHeightStart,InventoryWidth,InventoryHeight), "Inventory");
			if(GUI.Button(new Rect (InventoryWidthStart+475, InventoryHeightStart, 25, 25), "x")){
				qInventoryShown = true;
				InventoryOpen = false;
				MenuOpen = true;
			}
		}
		
		if (MenuOpen) {
			GUI.Box(new Rect(MenuWidthStart,MenuHeightStart,150,MenuSpacing*6+10), "Menu");
			if(GUI.Button(new Rect(MenuWidthStart+20,MenuHeightStart+MenuSpacing,110,20), "Load")) {
				//Load Game
			}
			if(GUI.Button(new Rect(MenuWidthStart+20,MenuHeightStart+2*MenuSpacing,110,20), "Save")) {
				//Save Game
			}
			if(GUI.Button(new Rect(MenuWidthStart+20,MenuHeightStart+3*MenuSpacing,110,20), "Journal")) {
				//Open journal
			}
			if(GUI.Button(new Rect(MenuWidthStart+20,MenuHeightStart+4*MenuSpacing,110,20), "Inventory")) {
				MenuOpen = !MenuOpen;
				qInventoryShown = false;
				InventoryOpen = true;
			}
			if(GUI.Button(new Rect(MenuWidthStart+20,MenuHeightStart+5*MenuSpacing,110,20), "Quit")) {
				//Save Game
			}
			
		}
		if (GUI.Button (new Rect (0, 0, 100, 50), "Menu")) {
			MenuOpen = !MenuOpen;
		}
		
	}//end OnGui

	public void AddQuickItem(Equipment e)
	{
		quickInventory.Add (e);
	}

	public void SetQuickItemSelection(int selection)
	{
		selectionGridInt = selection - 1;
	}

	public Equipment GetQuickItemSelected()
	{
		return quickInventory [selectionGridInt];
	}

    private void updateQuickItemGui()
	{
		for (int i = 0; i < selectionStrings.Length -1; i++)
			if (i > quickInventory.Count - 1)
				selectionStrings [i] = "item " + i; 
			else
				selectionStrings [i] = quickInventory [i] != null ? quickInventory [i].transform.name.ToString() : i.ToString() ;
	}
	
}


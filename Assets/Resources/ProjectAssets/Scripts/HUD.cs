//NEEDS CLEANING:  Hud quick select slots should be data-bound to underlying quickInventory data structure. Quick inventory should be made into
//custom data structure which autofills any 'empty' slots with our default 'empty hand' item.  This 'empty hand' item should contain the 'carry object'
//and 'grab object' scripts!  Without and empty hand, our character cannot grab things!

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour {

	public QuickInventory quickInventory = new QuickInventory(10);
	public Equipment e1;
	public Equipment e2;

	//Menu Variables
	private bool MenuOpen = false;
	int MenuWidthStart = (Screen.width/2)-75;
	int MenuHeightStart = (Screen.height/2)-125;
	int MenuSpacing = 30;
	
	//Menu Inventory Variables
	bool InventoryOpen = false;
	int InventorySelectionGrid = 0;
	int InventoryWidthStart = (Screen.width / 2) - 250;
	int InventoryHeightStart = (Screen.height / 2) - 250;
	int InventoryWidth = 500;
	int InventoryHeight = 500;
	
	//Quick Inventory Variables
	bool qInventoryShown = true;
	int selectionGridInt = 0;
	string[] selectionStrings = {"Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6"};

	//Codex Variables
	bool CodexOpen = false;
	int CodexWidthStart = (Screen.width / 2) - 400;
	int CodexHeightStart = (Screen.height / 2) - 250;
	int CodexWidth = 800;
	int CodexHeight = 500;


	void Start()
	{
		print (quickInventory.Count);
	}

	void Update() 
	{
		if (Input.GetKeyUp (KeyCode.F1) || Input.GetKeyUp(KeyCode.Escape)) {
			ToggleMenu();
		}
		//Screen.lockCursor = true;
		updateQuickItemGui ();
	}

	void OnGUI(){
		//GUI.Box (new Rect (Screen.width-200,0,200,25), "Air Quality Bar");
		//GUI.Box (new Rect (Screen.width-200,30,200,25), "% Bugs Found");
		
		//if (qInventoryShown) {
		//	selectionGridInt = GUI.SelectionGrid (new Rect (Screen.width / 2 - 250, Screen.height - 55, 500, 50), selectionGridInt, selectionStrings, 6);
		//}
		
		if (InventoryOpen) {
			
			Screen.lockCursor = false;
			GUI.Box (new Rect (InventoryWidthStart,InventoryHeightStart,InventoryWidth,InventoryHeight), "Inventory");
			InventorySelectionGrid = GUI.SelectionGrid (new Rect (InventoryWidthStart + 5, InventoryHeightStart + InventoryHeight - 55, InventoryWidth -5, 50 ), selectionGridInt, selectionStrings, 6);
			
			if(GUI.Button(new Rect (InventoryWidthStart+475, InventoryHeightStart, 25, 25), "x")){
				qInventoryShown = true;
				InventoryOpen = false;
				MenuOpen = true;
			}
		}

		if (CodexOpen) 
		{
			Screen.lockCursor = false;
			GUI.Box (new Rect (CodexWidthStart, CodexHeightStart, CodexWidth, CodexHeight), "Codex");
			
			GUI.BeginScrollView (new Rect (CodexWidthStart + 5, CodexHeightStart + 25, CodexWidth/2 + 100, CodexHeight/2), Vector2.zero, new Rect (CodexWidthStart + 5, CodexHeightStart + 25,  CodexWidth/2+75, CodexHeight*2));
			GUI.TextArea (new Rect (CodexWidthStart + 5, CodexHeightStart + 25, CodexWidth/2 + 100, CodexHeight/2), "Bug Information");
			GUI.EndScrollView();
			
			GUI.TextArea (new Rect (CodexWidthStart + 5, CodexHeightStart + CodexHeight/2 + 35, CodexWidth/2 + 100, CodexHeight/2-50), "Notes Here");
			
			GUI.BeginScrollView (new Rect (CodexWidthStart + CodexWidth/2 + 110, CodexHeightStart + 25 , CodexWidth/2-115, CodexHeight-30), Vector2.zero, new Rect (CodexWidthStart + CodexWidth/2 + 110, CodexHeightStart + 25,  CodexWidth/2-115, CodexHeight*2));
			GUI.TextArea (new Rect (CodexWidthStart + CodexWidth/2 + 110, CodexHeightStart + 25 , CodexWidth/2-115, CodexHeight-30), "Bug Information Index");
			GUI.EndScrollView();
			
			if (GUI.Button (new Rect (CodexWidthStart + CodexWidth - 25, CodexHeightStart, 25, 20), "x")) {
				qInventoryShown = true;
				InventoryOpen = false;
				CodexOpen = false;
				MenuOpen = true;
			}
		}		

		if (MenuOpen) 
		{
			Screen.lockCursor = false;
			Time.timeScale = 0;
			GUI.Box(new Rect(MenuWidthStart,MenuHeightStart,150,MenuSpacing*7+10), "Menu");
			if(GUI.Button(new Rect(MenuWidthStart+20,MenuHeightStart+MenuSpacing,110,20), "Load")) {
				//Load Game
			}
			if(GUI.Button(new Rect(MenuWidthStart+20,MenuHeightStart+2*MenuSpacing,110,20), "Save")) {
				//Save Game
			}
			if(GUI.Button(new Rect(MenuWidthStart+20,MenuHeightStart+3*MenuSpacing,110,20), "Codex")) {
				MenuOpen = !MenuOpen;
				qInventoryShown = false;
				InventoryOpen = false;
				CodexOpen = true;
			}
			if(GUI.Button(new Rect(MenuWidthStart+20,MenuHeightStart+4*MenuSpacing,110,20), "Inventory")) {
				MenuOpen = !MenuOpen;
				qInventoryShown = false;
				CodexOpen = false;
				InventoryOpen = true;
			}
			if(GUI.Button(new Rect(MenuWidthStart+20,MenuHeightStart+5*MenuSpacing,110,20), "Resume")) {
				ToggleMenu();
			}
			if(GUI.Button(new Rect(MenuWidthStart+20, MenuHeightStart+6*MenuSpacing, 110, 20), "Quit Game")){
				Application.Quit();//Will not exit when in editor or web mode, but will in actual game
			}
			
		}

		if (!MenuOpen && !CodexOpen && !InventoryOpen) 
		{
			Screen.lockCursor = true;
			Time.timeScale = 1;
		}
		
	}//end OnGui

	public void AddQuickItem(int i, Equipment e)
	{
		quickInventory.SetItem (i,e);
	}

	public void SetQuickItemSelection(int selection)
	{
		selectionGridInt = selection - 1;
	}

	public Equipment GetQuickItemSelected()
	{
		return quickInventory [selectionGridInt];
	}

	public void ToggleMenu()
	{
		MenuOpen = !MenuOpen;
		if (InventoryOpen) {
			InventoryOpen = false;
			qInventoryShown = true;
		}
		if (CodexOpen) {
			CodexOpen = false;
			qInventoryShown = true;
		}
		if (MenuOpen)
			SendMessage ("FreezePlayer");
		else
			SendMessage("UnfreezePlayer");
	}//end ToggleMenu

    private void updateQuickItemGui()
	{
		for (int i = 0; i < selectionStrings.Length -1; i++)
			if (i > quickInventory.Count - 1)
				selectionStrings [i] = "item " + i; 
			else
				selectionStrings [i] = quickInventory [i] != null ? quickInventory [i].transform.name.ToString() : i.ToString() ;
	}
	
}


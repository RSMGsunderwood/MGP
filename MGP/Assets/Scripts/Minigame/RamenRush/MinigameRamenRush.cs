using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameRamenRush : MinigameMain {

	public List<RamenRushOrder> ramenOrders;
	public List<string> soups;
	public List<string> meats;
	public List<string> toppings;



	//Initialize game on startup
	void Awake(){
		//Sets up orders
		string tempSoup = "";
		string tempMeat = "";
		string tempTopping = "";
		for (int i = 0; i < ramenOrders.Count; i++) {
			tempSoup = soups [Random.Range (0, soups.Count)];
			tempMeat = meats [Random.Range (0, meats.Count)];
			tempTopping = toppings [Random.Range (0, toppings.Count)];
			ramenOrders [i].AssignOrder (tempSoup, tempMeat, tempTopping);
		}
		//Subscribe to button inputs
		InputHandler.ButtonPressed += this.ButtonPress;
	}

	//When this screen is destroyed we need to unsubscribe
	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonPress;
	}

	//Temporary override
	public override void OnWin ()
	{

	}

	//Button input override
	public override void ButtonPress(int player, InputHandler.Buttons button){
		if (button == InputHandler.Buttons.y) {
				
		}
		if (button == InputHandler.Buttons.b) {
				
		}
		if (button == InputHandler.Buttons.x) {
				
		}
		if (button == InputHandler.Buttons.a) {
				
		}
	}
}

public class PlayerOrder{
	string soup;
	string meat;
	string topping;
}
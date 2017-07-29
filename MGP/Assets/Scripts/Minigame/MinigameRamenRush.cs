using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameRamenRush : MinigameMain {

	//Initialize game on startup
	void Awake(){
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTheJukes : MinigameMain {

	public List<TheJukes_CharControl> charReference = new List<TheJukes_CharControl>();
	List<TheJukes_CharControl> charControls = new List<TheJukes_CharControl>();

	//Initialize game on startup
	void Awake(){
		for (int i = 0; i < 4; i++) {
			
			if (GameHandler.instance.players [i].isPlaying) {
				
			}
		}
		InputHandler.ButtonPressed += this.ButtonPress;
		InputHandler.ButtonReleased += this.ButtonRelease;
	}

	//When this screen is destroyed we need to unsubscribe
	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonPress;
		InputHandler.ButtonReleased -= this.ButtonRelease;
	}

	//Button input override
	public override void ButtonPress(int player, InputHandler.Buttons button){
		if (button == InputHandler.Buttons.y) {
			charControls[player].PlayerInput(new Vector2(0,100));
		}
		if (button == InputHandler.Buttons.b) {
			charControls[player].PlayerInput(new Vector2(100,0));
		}
		if (button == InputHandler.Buttons.x) {
			charControls[player].PlayerInput(new Vector2(-100,0));
		}
		if (button == InputHandler.Buttons.a) {
			charControls[player].PlayerInput(new Vector2(0,-100));
		}
	}

	//Button release override
	public override void ButtonRelease(int player, InputHandler.Buttons button){
		if (button == InputHandler.Buttons.y) {
			charControls[player].PlayerInput(new Vector2(0,-100));
		}
		if (button == InputHandler.Buttons.b) {
			charControls[player].PlayerInput(new Vector2(-100,0));
		}
		if (button == InputHandler.Buttons.x) {
			charControls[player].PlayerInput(new Vector2(100,0));
		}
		if (button == InputHandler.Buttons.a) {
			charControls[player].PlayerInput(new Vector2(0,100));
		}
	}
}

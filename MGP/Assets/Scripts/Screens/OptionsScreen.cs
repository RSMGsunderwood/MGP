using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScreen : BaseScreen {

	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				GameHandler.instance.playerSpaces [i].TogglePlaying (true, false);
				if (GameHandler.instance.players [i].isVIP) {
					GameHandler.instance.playerSpaces [i].ToggleVIP (true);
				}
			}
		}
		InputHandler.ButtonPressed += this.ButtonWasHit;
	}

	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonWasHit;
	}

	public void Enable(){
		OnEnable ();
	}

	public void Disable(){
		OnDisable ();
	}

	public override void OnBack ()
	{

	}

	public override void OnEnable()
	{
		gameObject.SetActive (true);
		if(currentScreen&&currentScreen!=this)
			currentScreen.OnDisable();
		currentScreen = this;
	}

	public override void OnDisable ()
	{
		gameObject.SetActive (false);
	}

	public void ButtonWasHit(int player, InputHandler.Buttons button){
		if (button == InputHandler.Buttons.y) {

		}
		if (button == InputHandler.Buttons.b) {
			if (GameHandler.instance.players [player].isVIP) {
				ScreenHandler.instance.CreateScreen ("menuscreen", true);
			}
		}
		if (button == InputHandler.Buttons.x) {

		}
		if (button == InputHandler.Buttons.a) {

		}
	}
}

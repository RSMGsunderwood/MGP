using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScreen : BaseScreen {

	public List<PlayerArea> playerSpaces;
	public List<PlayerInputHandler> playerInputHandlers;

	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isVIP) {
				playerSpaces [i].ToggleVIP (true);
			}
		}
		InputHandler.ButtonPressed += this.ButtonWasHit;
	}

	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonWasHit;
	}

	void Update(){

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
			playerInputHandlers [0].ScrollTextRight ();
		}
		if (button == InputHandler.Buttons.x) {
			playerInputHandlers [0].ScrollTextLeft ();
		}
		if (button == InputHandler.Buttons.a) {
		}
	}
}

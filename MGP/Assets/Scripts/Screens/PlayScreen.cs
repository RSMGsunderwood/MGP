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
				playerSpaces [i].readyUpText.SetActive (false);
			}
		}
		InputHandler.ButtonPressed += this.ButtonWasHit;
	}

	void Start(){
		for (int i = 0; i < 4; i++) {
			if (!GameHandler.instance.players [i].isVIP) {
				playerInputHandlers [i].gameObject.SetActive (false);
			}
		}
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
			playerInputHandlers [player].ScrollTextLeft ();
		}
		if (button == InputHandler.Buttons.x) {
			playerInputHandlers [player].ScrollTextRight ();
		}
		if (button == InputHandler.Buttons.a) {
			if (GameHandler.instance.players [player].isPlaying) {
				playerInputHandlers [player].SelectText (player, playerSpaces [player]);
			} else {
				playerInputHandlers [player].gameObject.SetActive (true);
				GameHandler.instance.players [player].isPlaying = true;
				playerSpaces [player].readyUpText.SetActive (false);
			}
		}
	}
}

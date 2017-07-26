using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class MainMenuScreen : BaseScreen {

	public MenuScreenChoiceHandler menuInputHandler;	//Reference to menu interface
	public Image yellowButton;							//Reference to yellow button
	public TextMeshProUGUI yellowText;					//Reference to yellow button text
	//Initializes player spaces, menu selection, and button event subscription
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
	//Unsubscribes from button pressed when destroyed
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
	//When enabled, set as current
	public override void OnEnable()
	{
		gameObject.SetActive (true);
		if(currentScreen&&currentScreen!=this)
			currentScreen.OnDisable();
		currentScreen = this;
	}
	//Sets as inactive on disable
	public override void OnDisable ()
	{
		gameObject.SetActive (false);
	}
	//Button input handler
	public void ButtonWasHit(int player, InputHandler.Buttons button){
		//Goes back to title screen
		if (button == InputHandler.Buttons.y) {
			if (GameHandler.instance.playerSpaces [player].isVIP) {
				ScreenHandler.instance.CreateScreen ("titlescreen", true);
			}
		}
		//Lets VIP move meenu
		if (button == InputHandler.Buttons.b) {
			if (GameHandler.instance.playerSpaces [player].isVIP) {
				menuInputHandler.ScrollTextLeft ();
			}
		}
		if (button == InputHandler.Buttons.x) {
			if (GameHandler.instance.playerSpaces [player].isVIP) {
				menuInputHandler.ScrollTextRight ();
			}
		}
		//Lets VIP make menu selection
		if (button == InputHandler.Buttons.a) {
			if (GameHandler.instance.playerSpaces [player].isVIP) {
				menuInputHandler.SelectText ();
			}
		}
	}
}
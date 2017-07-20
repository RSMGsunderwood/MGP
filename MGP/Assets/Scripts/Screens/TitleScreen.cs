using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class TitleScreen : BaseScreen {

	public List<PlayerArea> playerSpaces;
	public Image greenButton;
	public Image redButton;
	public TextMeshProUGUI greenText;
	public TextMeshProUGUI redText;
	bool vipSet = false;
	//Subscribes to button press event and locks cursor if this isn't in editor
	void Awake(){
		InputHandler.ButtonPressed += this.ButtonWasHit;
		#if !UNITY_EDITOR
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		#endif
		for (int i = 0; i < 4; i++) {
			GameHandler.instance.players [i].isPlaying = false;
		}
	}
	//Unsubscribes from button event on destroy
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
	//Actives screen on enable
	public override void OnEnable()
	{
		gameObject.SetActive (true);
		if(currentScreen&&currentScreen!=this)
			currentScreen.OnDisable();
		currentScreen = this;
	}
	//Deactivates screen on disasable
	public override void OnDisable ()
	{
		gameObject.SetActive (false);
	}
	//Button input handler
	public void ButtonWasHit(int player, InputHandler.Buttons button){
		//Goes to menu if VIP presses A.  Makes player VIP if none have joined yet.
		if (button == InputHandler.Buttons.a) {
			if (vipSet) {
				if (playerSpaces [player].isVIP) {
					ScreenHandler.instance.CreateScreen ("menuscreen", true);
				}
			} else {
				GameHandler.instance.MakeVIP (player);
				GameHandler.instance.players [player].isPlaying = true;
				vipSet = true;
				playerSpaces [player].ToggleVIP (true);
				playerSpaces [player].TogglePlaying (true);
				redText.gameObject.SetActive (true);
				greenText.text = "Start Game";
				redButton.color = new Color (redButton.color.r, redButton.color.g, redButton.color.b, 1);
			}
		}
		//Revokes VIP if VIP presses b
		if (button == InputHandler.Buttons.b) {
			if (playerSpaces [player].isVIP) {
				GameHandler.instance.MakeVIP (5);
				GameHandler.instance.players [player].isPlaying = false;
				vipSet = false;
				playerSpaces [player].ToggleVIP (false);
				playerSpaces [player].TogglePlaying (false);
				redText.gameObject.SetActive (false);
				greenText.text = "Press GREEN to become the VIP";
				redButton.color = new Color (redButton.color.r, redButton.color.g, redButton.color.b, .5f);
			}
		}
	}
}
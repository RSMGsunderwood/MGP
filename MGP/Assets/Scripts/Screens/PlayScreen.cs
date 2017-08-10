using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayScreen : BaseScreen {

	public List<PlayScreenChoiceHandler> playerInputHandlers;
	int playersReady=0, activePlayers=0;

	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				GameHandler.instance.playerSpaces [i].TogglePlaying (true, true);
				GameHandler.instance.playerSpaces [i].readyUpText.SetActive (false);
				if (GameHandler.instance.players [i].isVIP) {
					GameHandler.instance.playerSpaces [i].ToggleVIP (true);
				}
				activePlayers++;
			} else {
				GameHandler.instance.playerSpaces [i].readyUpText.SetActive (true);
			}
		}
		InputHandler.ButtonPressed += this.ButtonWasHit;
	}

	void Start(){
		for (int i = 0; i < 4; i++) {
			if (!GameHandler.instance.players [i].isPlaying) {
				playerInputHandlers [i].gameObject.SetActive (false);
			}
		}
	}

	void OnDestroy(){
		for (int i = 0; i < 4; i++) {
			GameHandler.instance.playerSpaces [i].readyUpText.SetActive (false);
		}
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
			if (GameHandler.instance.playerSpaces [player].isVIP) {
				ScreenHandler.instance.CreateScreen ("menuscreen", true);
			}
		}
		if (button == InputHandler.Buttons.b) {
			if (GameHandler.instance.players [player].isPlaying) {
				playerInputHandlers [player].ScrollTextLeft (GameHandler.instance.playerSpaces [player]);
			}
		}
		if (button == InputHandler.Buttons.x) {
			if (GameHandler.instance.players [player].isPlaying) {
				playerInputHandlers [player].ScrollTextRight (GameHandler.instance.playerSpaces [player]);
			}
		}
		if (button == InputHandler.Buttons.a) {
			if (activePlayers == playersReady && GameHandler.instance.players [player].isVIP) {
				GameHandler.instance.ChooseGame (2, false);
				ScreenHandler.instance.CreateScreen ("minigamescreen", true);
			}
			if (GameHandler.instance.players [player].isPlaying) {
				if (playerInputHandlers [player].playerColorGO.activeInHierarchy) {
					playersReady++;
				}
				playerInputHandlers [player].SelectText (player, GameHandler.instance.playerSpaces [player]);
			} else {
				playerInputHandlers [player].gameObject.SetActive (true);
				GameHandler.instance.players [player].isPlaying = true;
				GameHandler.instance.playerSpaces [player].TogglePlaying (true, false);
				activePlayers++;
			}
		}
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isVIP) {
				if (activePlayers == playersReady) {
					GameHandler.instance.playerSpaces [i].readyUpText.GetComponent<TextMeshProUGUI> ().text = "Press <color=green>Green</color> to Start!";
					GameHandler.instance.playerSpaces [i].readyUpText.SetActive (true);
				} else {
					GameHandler.instance.playerSpaces [i].readyUpText.SetActive (false);
				}
			}
		}
	}

}

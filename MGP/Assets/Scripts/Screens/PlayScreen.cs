using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayScreen : BaseScreen {

	public List<PlayerArea> playerSpaces;
	public List<PlayerInputHandler> playerInputHandlers;
	int playersReady=0, activePlayers=0;

	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				playerSpaces [i].TogglePlaying (true);
				playerSpaces [i].readyUpText.SetActive (false);
				if (GameHandler.instance.players [i].isVIP) {
					playerSpaces [i].ToggleVIP (true);
				}
				activePlayers++;
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
			playerInputHandlers [player].ScrollTextLeft (playerSpaces [player]);
		}
		if (button == InputHandler.Buttons.x) {
			playerInputHandlers [player].ScrollTextRight (playerSpaces [player]);
		}
		if (button == InputHandler.Buttons.a) {
			if (activePlayers == playersReady && GameHandler.instance.players [player].isVIP) {
				GameHandler.instance.ChooseGame (0, true);
				ScreenHandler.instance.CreateScreen ("minigamescreen", true);
			}
			if (GameHandler.instance.players [player].isPlaying) {
				if (playerInputHandlers [player].playerColorGO.activeInHierarchy) {
					playersReady++;
				}
				playerInputHandlers [player].SelectText (player, playerSpaces [player]);
			} else {
				playerInputHandlers [player].gameObject.SetActive (true);
				GameHandler.instance.players [player].isPlaying = true;
				playerSpaces [player].TogglePlaying (true);
				activePlayers++;
			}
		}
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isVIP) {
				if (activePlayers == playersReady) {
					playerSpaces [i].readyUpText.GetComponent<TextMeshProUGUI> ().text = "Press <color=green>Green</color> to Start!";
					playerSpaces [i].readyUpText.SetActive (true);
				} else {
					playerSpaces [i].readyUpText.SetActive (false);
				}
			}
		}
	}
}

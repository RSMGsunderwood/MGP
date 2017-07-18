using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResultsScreen : BaseScreen {

	public List<PlayerArea> playerSpaces;
	public TextMeshProUGUI titleText;

	void Awake(){
		for (int i = 0; i < 4; i++) {
			if (GameHandler.instance.players [i].isPlaying) {
				playerSpaces [i].TogglePlaying (true);
				if (GameHandler.instance.players [i].isVIP) {
					playerSpaces [i].ToggleVIP (true);
				}
				playerSpaces [i].SetColor (GameHandler.instance.players [i].playerColor);
				playerSpaces [i].playerName.text = GameHandler.instance.players [i].playerName;
			} else {
				playerSpaces [i].playerName.text = "";
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
		
	}
}

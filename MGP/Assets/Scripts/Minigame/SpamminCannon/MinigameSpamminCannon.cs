using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameSpamminCannon : MinigameMain {

	public Color rCol, bCol, yCol, gCol;
	public Image displayButton;
	public SpamminCannonPlayerHud[] playerHuds;
	int currentChoice;

	//Initialize game on startup
	void Awake(){
		InputHandler.ButtonPressed += this.ButtonPress;
	}

	//When this screen is destroyed we need to unsubscribe
	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonPress;
	}

	IEnumerator ShowNewButton(){
		float lerp = 0;
		for (float i = 0; i < .2f; i += Time.deltaTime) {
			lerp = Mathf.SmoothStep (1, .8f, (.2f - i) / .2f);

			yield return null;
		}
	}

	public void ChooseRandomButton(){
		currentChoice = Random.Range (1, 5);
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
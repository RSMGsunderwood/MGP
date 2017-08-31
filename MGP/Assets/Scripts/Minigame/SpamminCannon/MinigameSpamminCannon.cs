using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameSpamminCannon : MinigameMain {

	public Color rCol, bCol, yCol, gCol;
	public Image displayButton;
	public SpamminCannonPlayerHud[] playerHuds;
	List<int> choices = new List<int>();
	int currentChoice;
	int checkChoice=0;

	//Initialize game on startup
	void Awake(){
		for (int i = 1; i < 5; i++) {
			choices.Add (i);
		}
		ChooseRandomButton ();
		InputHandler.ButtonPressed += this.ButtonPress;
	}

	//When this screen is destroyed we need to unsubscribe
	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonPress;
	}

	IEnumerator ShowNewButton(){
		float lerp = 0;
		RectTransform buttonTran = displayButton.GetComponent<RectTransform> ();
		for (float i = 0; i < .1f; i += Time.deltaTime) {
			lerp = Mathf.SmoothStep (.8f, 1, (.1f - i) / .1f);
			buttonTran.localScale = new Vector3 (lerp, lerp, lerp);
			yield return null;
		}
		buttonTran.localScale = new Vector3 (.8f, .8f, .8f);
		checkChoice = currentChoice;
		if (checkChoice == 1) {
			displayButton.color = rCol;
		} else if (checkChoice == 2) {
			displayButton.color = bCol;
		} else if (checkChoice == 3) {
			displayButton.color = yCol;
		} else if (checkChoice == 4) {
			displayButton.color = gCol;
		}
		for (float i = 0; i < .1f; i += Time.deltaTime) {
			lerp = Mathf.SmoothStep (.8f, 1, i/ .1f);
			buttonTran.localScale = new Vector3 (lerp, lerp, lerp);
			yield return null;
		}
		buttonTran.localScale = new Vector3 (1, 1, 1);
		yield return new WaitForSeconds (Random.Range (3f, 6f));
		ChooseRandomButton ();
	}

	public void ChooseRandomButton(){
		int tempChoice = 0;
		tempChoice = Random.Range (0, choices.Count);
		int lastChoice = currentChoice;
		currentChoice = choices [tempChoice];
		Debug.Log (currentChoice);
		if(lastChoice!=0)
			choices.Add (lastChoice);
		choices.Remove (currentChoice);
		StartCoroutine ("ShowNewButton");
	}

	//Button input override
	public override void ButtonPress(int player, InputHandler.Buttons button){
		bool passed = false;
		if (button == InputHandler.Buttons.y) {
			if (checkChoice == 3) {
				passed = true;
			}
		}
		if (button == InputHandler.Buttons.b) {
			if (checkChoice == 1) {
				passed = true;
			}
		}
		if (button == InputHandler.Buttons.x) {
			if (checkChoice == 2) {
				passed = true;
			}
		}
		if (button == InputHandler.Buttons.a) {
			if (checkChoice == 4) {
				passed = true;
			}
		}
		if (passed) {
			playerHuds [player].AddToGauge (player);
		}
	}

	public override void ButtonRelease(int player, InputHandler.Buttons button){
	}
}
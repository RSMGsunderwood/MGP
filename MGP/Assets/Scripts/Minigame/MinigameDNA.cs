using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameDNA : MinigameMain {

	public Transform textHolder;											//Text at top that we'll animate slightly to show rules
	public List<RectTransform> pIndicators = new List<RectTransform>();		//Player indiciators to show progression in game
	List<TextMeshProUGUI> dnaString = new List<TextMeshProUGUI>();			//List of each text object that we'll assign the string to
	string playString;														//The full string that the players are trying to progress through
	List<int> pProgress = new List<int>();									//Each player's progress in the minigame
	int playersFinished=0;													//Tracks how many players have finished so far

	//Initialize game on startup
	void Awake(){
		playString = "";	//Playstring starts at nothing
		string[] dna = new string [] {"CG","GC","AT","TA"};		//Here's the DNA pairs we'll be pulling from randomly
		List<int> rChoices = new List<int> ();		//Which DNA pairs are still available for choosing
		List<int> choices = new List<int> ();		//How many times we've selected a DNA pair
		//Initialize choice setup and player field
		for (int i = 0; i < 4; i++) {
			rChoices.Add (i);
			choices.Add (0);
			pProgress.Add (0);
			if (GameHandler.instance.players [i].isPlaying) {
				pIndicators [i].GetComponent<Image> ().color = GameHandler.instance.players [i].playerColor;
				pIndicators [i].gameObject.SetActive (true);
			} else {
				playersFinished++;
			}
		}
		//Create the full DNA string to play from
		for (int i = 0; i < 20; i++) {
			//Iterate through the text objects and put them into the list
			dnaString.Add(textHolder.GetChild(i*2).GetComponent<TextMeshProUGUI>());
			dnaString.Add(textHolder.GetChild((i*2)+1).GetComponent<TextMeshProUGUI>());
			//Randomly choose a DNA pair to put into these slots
			int m = Random.Range (0, rChoices.Count);
			playString += dna [rChoices[m]];
			dnaString [playString.Length - 2].text = playString [playString.Length - 2].ToString();
			dnaString [playString.Length - 1].text = playString [playString.Length - 1].ToString();
			choices [m]++;
			//If we've already selected this pair 5 times then let's not use it anymore
			if (choices [m] == 5) {
				rChoices.RemoveAt (m);
				choices.RemoveAt (m);
			}
		}
		//Subscribe to button inputs
		InputHandler.ButtonPressed += this.ButtonPress;
	}

	//When this screen is destroyed we need to unsubscribe
	void OnDestroy(){
		InputHandler.ButtonPressed -= this.ButtonPress;
	}

	//Temporary override
	public override void OnWin ()
	{
		
	}

	//Button input override
	public override void ButtonPress(int player, InputHandler.Buttons button){
		bool passed = false;
		if (pIndicators [player].gameObject.activeInHierarchy) {
			//G
			if (button == InputHandler.Buttons.y) {
				if (playString [pProgress [player]].ToString () == "G") {
					passed = true;
				}
			}
			//C
			if (button == InputHandler.Buttons.b) {
				if (playString [pProgress [player]].ToString () == "C") {
					passed = true;
				}
			}
			//A
			if (button == InputHandler.Buttons.x) {
				if (playString [pProgress [player]].ToString () == "A") {
					passed = true;
				}
			}
			//T
			if (button == InputHandler.Buttons.a) {
				if (playString [pProgress [player]].ToString () == "T") {
					passed = true;
				}
			}
			//If the player chose the right button, then progress them forward.
			//If the player finished the sequence then save their time and disable thier indicator.
			if (passed) {
				pProgress [player]++;
				GameHandler.instance.players [player].pointScore++;
				if (pProgress [player] < playString.Length) {
					pIndicators [player].anchoredPosition = new Vector2 (dnaString [pProgress [player]].GetComponent<RectTransform> ().anchoredPosition.x, dnaString [pProgress [player]].GetComponent<RectTransform> ().anchoredPosition.y - 15 - (2 * player));
				} else {
					pIndicators [player].gameObject.SetActive (false);
					GameHandler.instance.players [player].timeScore = GameHandler.instance.timer;
					playersFinished++;
				}
			//If the player hit the wrong button then they're done and get no points or time score.
			} else {
				pIndicators [player].gameObject.SetActive (false);
				GameHandler.instance.players [player].pointScore = 0;
				GameHandler.instance.players [player].timeScore = 00.00f;
				playersFinished++;
			}
			//If all players are done, end the game.
			if (playersFinished == 4) {
				GameHandler.instance.CalculateWinner ();
				ScreenHandler.instance.CreateScreen ("resultsscreen", true);
			}
		}
	}
}